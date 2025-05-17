using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Core.Entities.Application;
using Website.Core.Specifications.Product;

namespace Website.Core.ServiceInterfaces;

public interface IEventService
{
		IReadOnlyList<Event> GetAllEvents(EventParams specParams);
		Event? GetEventById(string id);
		int GetCount(EventParams specParams);
		public bool CancelEventRegistration(string eventId, string userId);
		public bool RegisterUserForEvent(string eventId, string userId);

		Task<bool> Add(Event @event);
		Task<bool> Update(Event @event);
		Task<bool> Delete(Event @event);
}
