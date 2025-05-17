using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Website.Core.Entities.Application;
using Website.Core.Specifications.Product;

namespace Website.Core.Specifications.Events
{
	public class EventIncludingTagAndCategory : Specification<Event>
	{
		public EventIncludingTagAndCategory(EventParams eventParams)
			: base(e =>
				 (string.IsNullOrEmpty(eventParams.Search) || e.Title.ToLower().Contains(eventParams.Search)) &&
				 (string.IsNullOrEmpty(eventParams.TagId) || e.EventTags.Any(e => e.TagId.ToString() == eventParams.TagId) &&
				 (string.IsNullOrEmpty(eventParams.CategoryId) || e.CategoryId.ToString() == eventParams.CategoryId) &&
				 (string.IsNullOrEmpty(eventParams.Date.ToString()) || e.StartDate == eventParams.Date) &&
				 (!e.Price.HasValue) || (e.Price > eventParams.MinPrice && e.Price < eventParams.MaxPrice))
				 )
		{
			AddIncludes();
			ApplyPagination(eventParams.PageSize, eventParams.PageIndex);
		}

		public EventIncludingTagAndCategory(Expression<Func<Event, bool>> criteria) : base(criteria)
		{
			AddIncludes();
		}

		////////////////////////////////////////////////////
		private void AddIncludes()
		{
			Includes.Add(p => p.Category);
			Includes.Add(p => p.EventTags);
		}

		private void ApplyPagination(int? pageSize, int? pageIndex)
		{
			Skip = (pageIndex - 1) * pageSize;
			Take = pageSize;
		}

	}
}