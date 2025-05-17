using Website.Core.Entities.Application;
using Website.Core.Specifications.Categories;

namespace Website.Core.ServiceInterfaces
{
	public interface ICategoryService
	{
		Task<bool> Add(Category category);
		Task<bool> Update(Category category);
		Task<bool> Delete(Category category);

		Category GetCategoryById(string Id);
		int GetCount(CategoryParams categoryParams);
		IReadOnlyList<Category> GetAllCategories(CategoryParams categoryParams);
	}
}
