using Website.Core.Entities.Application;

namespace Website.Core.Specifications.Categories;

public class CategoriesWithFilterationForCountSpec : Specification<Category>
{
    public CategoriesWithFilterationForCountSpec(CategoryParams categoryParams) 
		: base(
	  e => (string.IsNullOrEmpty(categoryParams.Search) || e.Name.ToLower().Contains(categoryParams.Search))
			 )
	{
        
    }
}
