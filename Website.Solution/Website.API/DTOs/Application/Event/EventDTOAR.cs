using Website.Core.Entities.Application;

namespace Website.API.DTOs.Application.Event
{
	public class EventDTOAR : EventDTO
	{
		public string TitleAR { get; set; }
		public string DescriptionAR { get; set; }

		public string ShortDescriptionAR { get; set; }
		public string LocationAR { get; set; } // map
		public string StatusAR { get; set; }

		public string CategoryNameAR { get; set; }
		public ICollection<string> AttendeesNamesAR { get; set; } = new List<string>();
		public ICollection<string> EventTagsNamesAR { get; set; } = new List<string>();
	}
}
