
using Website.Core.Entities.Application;
using Website.Core.Specifications.Product;

namespace Website.Core.Specifications.Events
{
	public class EventsWithFilterationForCountSpec : Specification<Event>
	{
		public EventsWithFilterationForCountSpec(EventParams eventParams)
	: base(e =>
		 (string.IsNullOrEmpty(eventParams.Search) || e.Title.ToLower().Contains(eventParams.Search)) &&
		 (string.IsNullOrEmpty(eventParams.TagId) || e.EventTags.Any(e => e.TagId.ToString() == eventParams.TagId) &&
		 (string.IsNullOrEmpty(eventParams.CategoryId) || e.CategoryId.ToString() == eventParams.CategoryId) &&
		 (string.IsNullOrEmpty(eventParams.Date.ToString()) || e.StartDate == eventParams.Date) &&
		 (!e.Price.HasValue) || (e.Price > eventParams.MinPrice && e.Price < eventParams.MaxPrice))
		 )
		{

		}
	}
}
