using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Website.Core.Entities.Application;
using Website.Core.Specifications.Product;

namespace Website.Core.Specifications.Categories
{
	public class CategoryIncludingEventsSpec : Specification<Category>
	{
		public CategoryIncludingEventsSpec(CategoryParams categoryParams)
			: base(
				  e =>(string.IsNullOrEmpty(categoryParams.Search) || e.Name.ToLower().Contains(categoryParams.Search))
				  )
		{
			AddIncludes();
			ApplyPagination(categoryParams.PageSize, categoryParams.PageIndex);
		}

		public CategoryIncludingEventsSpec(Expression<Func<Category, bool>> criteria) : base(criteria)
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
