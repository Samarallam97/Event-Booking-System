using Website.Core.Entities.Identity;

namespace Website.Core.Entities.Application;

public class EventAttendee : BaseEntity
{
    public string EventId { get; set; }
    public string UserId { get; set; }

    //public string RegisterationStatus { get; set; }

    // Navigation property
    public Event Event { get; set; }
    public ApplicationUser User { get; set; }
}