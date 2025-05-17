
namespace Website.Core.Entities.Application;

public class Category : BaseEntity
{
    public string Name { get; set; }
	public string NameAR { get; set; }
	public string Description { get; set; }
	public string DescriptionAR { get; set; }

	public string IconUrl { get; set; }
    public string Color { get; set; }

    // Navigation properties
    public ICollection<Event> Events { get; set; } = new List<Event>();
}