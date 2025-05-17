using Website.Core.Entities.Application;

namespace Website.Core.Specifications.Tags;

public class TagsWithFilterationForCountSpec : Specification<Tag>
{
    public TagsWithFilterationForCountSpec(TagsParams tagsParams) 
		: base(
	  e => (string.IsNullOrEmpty(tagsParams.Search) || e.Name.ToLower().Contains(tagsParams.Search))
			 )
	{
        
    }
}
