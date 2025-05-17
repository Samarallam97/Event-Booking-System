using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Core.Entities.Application;
using Website.Core;
using Website.Core.ServiceInterfaces;
using Website.Core.Specifications.Product;
using Website.Core.Specifications.Events;
using Website.Core.RepositoryInterfaces;
using Microsoft.Extensions.Logging;

namespace Website.Service
{
	public class EventService : IEventService
	{
		private readonly IUnitOfWork _unitOfWork;

		public EventService(IUnitOfWork unitOfWork)
		{
			_unitOfWork=unitOfWork;
		}
		public IReadOnlyList<Event> GetAllEvents(EventParams specParams)
		{
			var spec = new EventIncludingTagAndCategory(specParams);

			var events = _unitOfWork.Repository<Event>().GetAllWithSpec(spec);

			return events;
		}

		public int GetCount(EventParams specParams)
		{
			var countSpec = new EventsWithFilterationForCountSpec(specParams);

			var count = _unitOfWork.Repository<Event>().GetAll().Count();

			return count;
		}


		public Event? GetEventById(string id)
		{
			var spec = new EventIncludingTagAndCategory(p => p.Id == id);

			var product = _unitOfWork.Repository<Event>().GetEntityWithSpec(spec);

			return product;
		}

		public bool CancelEventRegistration(string eventId, string userId)
		{
			var _eventRepo = _unitOfWork.Repository<Event>() as IEventRepository;
			
			return _eventRepo.CancelEventRegistration(eventId, userId);
		}

		public bool RegisterUserForEvent(string eventId, string userId)
		{
			var _eventRepo = _unitOfWork.Repository<Event>() as IEventRepository;

			return _eventRepo.RegisterUserForEvent(eventId, userId);
		}

		public async Task<bool> Add(Event @event)
		{
			_unitOfWork.Repository<Event>().Add(@event);

			var result = await _unitOfWork.CompleteAsync();

			if(result > 0)
				return true;

			return false;
		}

		public async Task<bool> Update(Event @event)
		{
			_unitOfWork.Repository<Event>().Update(@event);

			var result = await _unitOfWork.CompleteAsync();

			if (result > 0)
				return true;

			return false;

		}

		public async Task<bool> Delete(Event @event)
		{
			_unitOfWork.Repository<Event>().Delete(@event);

			var result = await _unitOfWork.CompleteAsync();

			if (result > 0)
				return true;

			return false;
		}
	}
}
