
using Website.Core;
using Website.Core.Entities.Application;
using Website.Core.ServiceInterfaces;
using Website.Core.Specifications.Categories;

namespace Website.Service
{
	public class CategoryService : ICategoryService
	{
		private readonly IUnitOfWork _unitOfWork;

		public CategoryService(IUnitOfWork unitOfWork)
        {
			_unitOfWork=unitOfWork;
		}
        public async Task<bool> Add(Category category)
		{
			_unitOfWork.Repository<Category>().Add(category);
			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}

		public async Task<bool> Update(Category category)
		{

			_unitOfWork.Repository<Category>().Update(category);

			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}
		public async Task<bool> Delete(Category category)
		{
			_unitOfWork.Repository<Category>().Delete(category);

			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}

		public IReadOnlyList<Category> GetAllCategories(CategoryParams categoryParams)
		{
			var spec = new CategoryIncludingEventsSpec(categoryParams);

			var categories = _unitOfWork.Repository<Category>().GetAllWithSpec(spec);

			return categories;
		}

		public Category GetCategoryById(string Id)
		{
			return _unitOfWork.Repository<Category>().GetEntityById(Id);
		}

		public int GetCount(CategoryParams categoryParams)
		{
			var spec = new CategoriesWithFilterationForCountSpec(categoryParams);
			
			int count = _unitOfWork.Repository<Category>().GetAllWithSpec(spec).Count();

			return count;
		}


	}
}
