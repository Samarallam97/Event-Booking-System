
using Website.Core;
using Website.Core.Entities.Application;
using Website.Core.ServiceInterfaces;
using Website.Core.Specifications.Categories;
using Website.Core.Specifications.Tags;

namespace Website.Service
{
	public class TagService : ITagService
	{
		private readonly IUnitOfWork _unitOfWork;

		public TagService(IUnitOfWork unitOfWork)
        {
			_unitOfWork=unitOfWork;
		}
        public async Task<bool> Add(Tag tag)
		{
			_unitOfWork.Repository<Tag>().Add(tag);
			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}

		public async Task<bool> Update(Tag tag)
		{

			_unitOfWork.Repository<Tag>().Update(tag);

			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}


		public async Task<bool> Delete(Tag tag)
		{

			_unitOfWork.Repository<Tag>().Delete(tag);

			var result = await _unitOfWork.CompleteAsync();

			return result > 0 ? true : false;
		}

		public IReadOnlyList<Tag> GetAllTags(TagsParams tagsParams)
		{
			var spec = new TagsIncludingEventsSpec(tagsParams);

			var tags = _unitOfWork.Repository<Tag>().GetAllWithSpec(spec);

			return tags;
		}

		public int GetCount(TagsParams tagParams)
		{
			var spec = new TagsWithFilterationForCountSpec(tagParams);

			int count = _unitOfWork.Repository<Tag>().GetAllWithSpec(spec).Count();

			return count;
		}

		public Tag GetTagById(string Id)
		{
			return _unitOfWork.Repository<Tag>().GetEntityById(Id);
		}


	}
}
