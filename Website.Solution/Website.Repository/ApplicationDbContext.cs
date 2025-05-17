using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Website.Core.Entities.Identity;
using Website.Core.Entities.Application;


namespace Website.Repository;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Tag> Tags { get; set; }
	public DbSet<Event> Events { get; set; }
	public DbSet<EventAttendee> EventAttendees { get; set; }
	public DbSet<EventTag> EventTags { get; set; }
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
            
    }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);


		builder.Entity<EventTag>()
			.HasKey(et => new { et.EventId, et.TagId });

		builder.Entity<EventTag>()
			.HasOne(et => et.Event)
			.WithMany(e => e.EventTags)
			.HasForeignKey(et => et.EventId);

		builder.Entity<EventTag>()
			.HasOne(et => et.Tag)
			.WithMany(t => t.Events)
			.HasForeignKey(et => et.TagId);

		builder.Entity<EventAttendee>()
			.HasOne(ea => ea.Event)
			.WithMany(e => e.Attendees)
			.HasForeignKey(ea => ea.EventId);

		builder.Entity<Event>()
			.HasOne(e => e.Category)
			.WithMany(c => c.Events)
			.HasForeignKey(e => e.CategoryId);

		builder.Entity<Event>().Property(e => e.Price).HasPrecision(8, 2);
	}
}
