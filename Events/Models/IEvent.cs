﻿namespace Events.Models
{
    using System;
    using System.Collections.Generic;

    public interface IEvent
    {
        Guid Id { get; }
        string Category { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        DateTime ScheduledDate { get; set; }
        IAddress Location { get; set; }
        bool AddParticipant(IParticipant participant);
        bool RemoveParticipant(string email);
        bool IsParticipantSignedUp(string email);
        void Update(IEvent updatedEvent);
        List<IParticipant> GetParticipants();
    }
}
