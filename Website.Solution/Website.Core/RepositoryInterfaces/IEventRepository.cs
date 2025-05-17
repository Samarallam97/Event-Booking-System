
using Website.Core.Entities.Application;

namespace Website.Core.RepositoryInterfaces;

public interface IEventRepository : IGenericRepository<Event>
{
	bool RegisterUserForEvent(string eventId, string userId);
	bool CancelEventRegistration(string eventId, string userId);
}
