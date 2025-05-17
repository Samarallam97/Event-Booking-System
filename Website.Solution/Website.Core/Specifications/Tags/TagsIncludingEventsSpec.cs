using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Website.Core.Entities.Application;
using Website.Core.Specifications.Product;

namespace Website.Core.Specifications.Tags
{
	public class TagsIncludingEventsSpec : Specification<Tag>
	{
		public TagsIncludingEventsSpec(TagsParams tagsParams)
			: base(
				  e =>(string.IsNullOrEmpty(tagsParams.Search) || e.Name.ToLower().Contains(tagsParams.Search))
				  )
		{
			AddIncludes();
			ApplyPagination(tagsParams.PageSize, tagsParams.PageIndex);
		}

		public TagsIncludingEventsSpec(Expression<Func<Tag, bool>> criteria) : base(criteria)
		{
			AddIncludes();
		}

		////////////////////////////////////////////////////
		private void AddIncludes()
		{
			Includes.Add(c => c.Events);
		}

		private void ApplyPagination(int? pageSize, int? pageIndex)
		{
			Skip = (pageIndex - 1) * pageSize;
			Take = pageSize;
		}
	}
}
