namespace Website.API.DTOs.Application.Event
{
    public class EventDTOAR : EventDTO
    {
        public string TitleAR { get; set; }
        public string DescriptionAR { get; set; }

        public string ShortDescriptionAR { get; set; }
        public string LocationAR { get; set; }
        public string StatusAR { get; set; }

        public string CategoryNameAR { get; set; }
        public ICollection<string> AttendeesNamesAR { get; set; } = [];
        public ICollection<string> EventTagsNamesAR { get; set; } = [];
    }
}
