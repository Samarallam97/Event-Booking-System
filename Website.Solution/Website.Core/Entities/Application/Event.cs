
namespace Website.Core.Entities.Application;

public class Event : BaseEntity
{
    public string Title { get; set; }
	public string TitleAR { get; set; }

	public string Description { get; set; }
	public string DescriptionAR { get; set; }

	public string ShortDescription { get; set; }
	public string ShortDescriptionAR { get; set; }

	public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; } // map
	public string LocationAR { get; set; } // map

	public string VenueAddress { get; set; }
    public string MainImageUrl { get; set; }
    public int? MaxAttendees { get; set; }
    public int CurrentCount { get; set; } 
    public decimal? Price { get; set; }
    public string Status { get; set; }
	public string StatusAR { get; set; }

	public string CategoryId { get; set; }

    // Navigation properties
    public Category Category { get; set; }
    public ICollection<EventAttendee> Attendees { get; set; } = new List<EventAttendee>();
    public ICollection<EventTag> EventTags { get; set; } = new List<EventTag>();
}