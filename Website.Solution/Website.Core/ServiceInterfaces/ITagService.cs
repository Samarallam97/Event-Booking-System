using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Core.Entities.Application;
using Website.Core.Specifications.Categories;
using Website.Core.Specifications.Tags;

namespace Website.Core.ServiceInterfaces
{
	public interface ITagService
	{
		Task<bool> Add(Tag tag);
		Task<bool> Update( Tag tag);
		Task<bool> Delete(Tag tag);

		Tag GetTagById(string Id);
		int GetCount(TagsParams tagParams);
		IReadOnlyList<Tag> GetAllTags(TagsParams tagParams);
	}
}
