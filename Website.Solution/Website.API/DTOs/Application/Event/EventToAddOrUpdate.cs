using Website.Core.Entities.Application;

namespace Website.API.DTOs.Application.Event
{
	public class EventToAddOrUpdate : EventDTO
	{
		public string Title { get; set; }
		public string TitleAR { get; set; }

		public string Description { get; set; }
		public string DescriptionAR { get; set; }

		public string ShortDescription { get; set; }
		public string ShortDescriptionAR { get; set; }
		public string Location { get; set; } // map
		public string LocationAR { get; set; } // map
		public string Status { get; set; }
		public string StatusAR { get; set; }

        public string CategoryId { get; set; }
        public ICollection<string> EventTagsIds { get; set; } = new List<string>();

	}
}
