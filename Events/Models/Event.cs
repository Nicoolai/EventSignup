using System;

namespace Events.Models
{
    class Event : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ScheduledDate { get; set; }
        public IAddress Location { get;set; }
    }
}
