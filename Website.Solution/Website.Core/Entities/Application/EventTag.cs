namespace Website.Core.Entities.Application;

public class EventTag
{
    public string EventId { get; set; }
    public string TagId { get; set; }

    // Navigation properties
    public Event Event { get; set; }
    public Tag Tag { get; set; }
}