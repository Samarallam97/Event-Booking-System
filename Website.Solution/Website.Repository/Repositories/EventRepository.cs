using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Core.Entities.Application;
using Website.Core.RepositoryInterfaces;

namespace Website.Repository.Repositories
{
	public class EventRepository : GenericRepository<Event>  , IEventRepository
	{
        public EventRepository(ApplicationDbContext context):base(context) { }

		public bool RegisterUserForEvent(string eventId, string userId)
		{
			var @event = GetEntityById(eventId);
			if (@event != null)
				return false;

			var user = GetEntityById(userId);
			if (user != null)
				return false;

			var eventAttendee = new EventAttendee()
			{
				EventId = eventId,
				UserId = userId,
			};

			@event.Attendees.Add(eventAttendee);
			@event.CurrentCount +=1;

			var result = _context.SaveChanges();

			if(result != 0) 
				return true;

			return false;
		}

		public bool CancelEventRegistration(string eventId, string userId)
		{

			var @event = GetEntityById(eventId);
			if (@event != null)
				return false;

			var user = GetEntityById(userId);
			if (user != null)
				return false;

			var eventAttendee = new EventAttendee()
			{
				EventId = eventId,
				UserId = userId,
			};

			@event.Attendees.Remove(eventAttendee);
			@event.CurrentCount -=1;

			var result = _context.SaveChanges();

			if (result != 0)
				return true;

			return false;

		}
	}
}
