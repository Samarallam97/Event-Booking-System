using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Website.Core.Entities.Application;
using Website.Core.Entities.Identity;

namespace Website.Repository.DataSeeding;

public class DataSeeder
{
	public static void Seed(ApplicationDbContext _context)
	{
		if (_context.Categories?.Count() == 0)
		{
			var categoriesAsString = File.ReadAllText("../Website.Repository/DataSeeding/Files/Category.json");
			var categories = JsonSerializer.Deserialize<List<Category>>(categoriesAsString);

			foreach (var category in categories)
			{
				_context.Categories.Add(category);
			}
			_context.SaveChanges();

		}

		if (_context.Tags?.Count() == 0)
		{
			var tagsAsString = File.ReadAllText("../Website.Repository/DataSeeding/Files/Tags.json");
			var tags = JsonSerializer.Deserialize<List<Tag>>(tagsAsString);

			foreach (var tag in tags)
			{
				_context.Tags.Add(tag);
			}
			_context.SaveChanges();

		}

		if (_context.Events?.Count() == 0)
		{
			var eventsAsString = File.ReadAllText("../Website.Repository/DataSeeding/Files/Events.json");
			var events = JsonSerializer.Deserialize<List<Event>>(eventsAsString);

			foreach (var @event in events)
			{
				_context.Events.Add(@event);
			}
			_context.SaveChanges();

		}

		if (_context.Users?.Count() == 0)
		{
			var usersAsString = File.ReadAllText("../Website.Repository/DataSeeding/Files/Users.json");
			var users = JsonSerializer.Deserialize<List<ApplicationUser>>(usersAsString);

			foreach (var user in users)
			{
				_context.Users.Add(user);
			}
			_context.SaveChanges();

		}
		if (_context.EventTags?.Count() == 0)
		{
			var eventTagsAsString = File.ReadAllText("../Website.Repository/DataSeeding/Files/EventTag.json");
			var eventTags = JsonSerializer.Deserialize<List<EventTag>>(eventTagsAsString);

			foreach (var @event in eventTags)
			{
				_context.EventTags.Add(@event);
			}
			_context.SaveChanges();

		}

		//if (_context.EventAttendees?.Count() == 0)
		//{
		//	var EventAttendeesAsString = File.ReadAllText("../Website.Repository/DataSeeding/Files/EventAttendee.json");
		//	var EventAttendees = JsonSerializer.Deserialize<List<EventAttendee>>(EventAttendeesAsString);

		//	foreach (var @event in EventAttendees)
		//	{
		//		_context.EventAttendees.Add(@event);
		//	}
		//	_context.SaveChanges();

		//}


	}
}