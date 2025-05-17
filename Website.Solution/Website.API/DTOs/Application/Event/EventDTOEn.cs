namespace Website.API.DTOs.Application.Event
{
	public class EventDTOEn : EventDTO
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string ShortDescription { get; set; }
		public string Location { get; set; }
		public string Status { get; set; }

		public string CategoryName{ get; set; }
		public ICollection<string> AttendeesNames { get; set; } = new List<string>();
		public ICollection<string> EventTagsNames { get; set; } = new List<string>();
	}
}
