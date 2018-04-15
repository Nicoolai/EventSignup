using System;

namespace Events.Models
{
    interface IEvent
    {
        Guid Id { get; }
        string Category { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        DateTime ScheduledDate { get; set; }
        IAddress Location { get; set; }
    }
}
