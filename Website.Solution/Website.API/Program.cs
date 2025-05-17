
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Website.API.Errors;
using Website.API.Extensions;
using Website.API.Middlewares;
using Website.Repository;
using Website.Repository.DataSeeding;

namespace Website.API;

public class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		#region Services Container

		builder.Services.AddControllers();

		#region Databases

		builder.Services.AddDbContext<ApplicationDbContext>(options =>
		{
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
		});

		builder.Services.AddSingleton<IConnectionMultiplexer>(provider =>
		{
			var connectionString = builder.Configuration.GetConnectionString("Redis")!;
			return ConnectionMultiplexer.Connect(connectionString);
		});

		#endregion

		builder.Services.AddDependencyInjectionServices(builder.Configuration);

		builder.Services.AddCors(options =>
		{
			options.AddPolicy("MyPolicy", options =>
			{
				options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
			});
		});

		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		#region Validation Error Handling

		builder.Services.Configure<ApiBehaviorOptions>(options =>
		{
			options.InvalidModelStateResponseFactory = (actionContext) =>
			{
				var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
													.SelectMany(p => p.Value.Errors)
													.Select(e => e.ErrorMessage)
													.ToArray();

				var validationErrorResponse = new ValidationErrorResponse()
				{
					Errors = errors
				};

				return new BadRequestObjectResult(validationErrorResponse);
			};
		});
		#endregion

		#endregion

		var app = builder.Build();

		#region Update-Database

		var scope = app.Services.CreateScope();
		var serviceProvider = scope.ServiceProvider;

		var context = serviceProvider.GetService<ApplicationDbContext>() !;
		var loggerFactory = serviceProvider.GetService<ILoggerFactory>() !;

		try
		{
			await context.Database.MigrateAsync();
		}
		catch (Exception ex)
		{
			var logger = loggerFactory.CreateLogger<Program>();
			logger.LogError(ex, "An error has occurred while updating the database");
		}
		#endregion

		#region DataSeeding

		DataSeeder.Seed(context);

		#endregion

		#region Middlewares

		#region ExceptionHandling
		app.UseMiddleware<ExceptionHandlingMiddleware>();
		#endregion

		app.UseSwagger();
		app.UseSwaggerUI();
		

		app.UseStaticFiles();

		#region Not Found EndPoint Handling
		app.UseStatusCodePagesWithReExecute("/error/{0}");
		#endregion

		app.UseCors("MyPolicy");

		app.UseHttpsRedirection();

		app.UseAuthentication();

		app.UseAuthorization();

		app.MapControllers(); 
		#endregion

		app.Run();
	}
}
