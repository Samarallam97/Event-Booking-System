using Website.Core.Entities.Application;

namespace Website.API.DTOs.Application.Event;

public class EventDTO
{
    public string Id { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }

	public string VenueAddress { get; set; }
	public string MainImageUrl { get; set; }

	public int? MaxAttendees { get; set; }
	public int CurrentCount { get; set; }
	public decimal? Price { get; set; }

}
