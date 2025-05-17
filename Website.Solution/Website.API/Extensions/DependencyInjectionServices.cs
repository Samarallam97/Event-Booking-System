using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Website.Repository;
using Website.API.Helpers;
using Website.Core;
using Website.Service;
using Website.Core.ServiceInterfaces;
using Website.Core.Entities.Identity;
using Website.Core.RepositoryInterfaces;
using Website.Repository.Repositories;

namespace Website.API.Extensions;

public static class DependencyInjectionServices
{
	public static IServiceCollection AddDependencyInjectionServices(this IServiceCollection Services, IConfiguration configuration)
	{
		Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));


		Services.AddScoped(typeof(IAuthService), typeof(AuthService));
		Services.AddScoped(typeof(IEventService), typeof(EventService));
		Services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
		Services.AddScoped(typeof(ITagService), typeof(TagService));

		Services.AddAutoMapper(m => m.AddProfile(typeof(MappingProfiles)));

		#region Identity

		Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

		Services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

		}).AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters()
			{
				ValidateAudience = true,
				ValidAudience = configuration["JWT:Audience"],
				ValidateIssuer = true,
				ValidIssuer = configuration["JWT:Issuer"],
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"] !)),
				ValidateLifetime = true,
				ClockSkew = TimeSpan.FromDays(double.Parse(configuration["JWT:ExpireInDays"] !))
			};
		});

		Services.AddAuthorization(options =>
		{
			options.AddPolicy("AdminOnly", policy =>
				policy.RequireRole("Admin"));

			options.AddPolicy("UserOrAdmin", policy =>
				policy.RequireRole("User", "Admin"));
		});
		#endregion


		return Services;
	} 
}
