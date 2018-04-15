namespace Events.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Event : IEvent
    {
        private List<IParticipant> participants;

        public Event()
        {
            this.participants = new List<IParticipant>();
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ScheduledDate { get; set; }
        public IAddress Location { get; set; }

        public bool AddParticipant(IParticipant participant)
        {
            this.participants.Add(participant);
            return this.participants.Any(p => p == participant);
        }

        public bool RemoveParticipant(string email)
        {
            this.participants.Remove(this.participants.Single(p => p.Email.ToUpper() == email.ToUpper()));
            return this.participants.Any(p => p.Email.ToUpper() != email.ToUpper());
        }

        public bool IsParticipantSignedUp(IParticipant participant)
        {
            return this.participants.Any(p => p == participant);
        }

        public bool IsParticipantSignedUp(string email)
        {
            return this.participants.Any(p => p.Email.ToUpper() == email.ToUpper());
        }
    }
}