
namespace Website.Core.Entities.Application;

public class Tag : BaseEntity
{
    public string Name { get; set; }
	public string NameAR { get; set; }


	// Navigation properties
	public ICollection<EventTag> Events { get; set; } = new List<EventTag>();
}