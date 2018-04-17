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
            // Might be a good idea to actually check if participant is already signed up, in here.
            this.participants.Add(participant);
            return this.participants.Any(p => p == participant);
        }

        public bool RemoveParticipant(string email)
        {
            // Might be a good idea to check if participant is actually signed up, in here.
            this.participants.Remove(this.participants.Single(p => p.Email.ToUpper() == email.ToUpper()));
            return this.participants.Any(p => p.Email.ToUpper() != email.ToUpper());
        }

        public bool IsParticipantSignedUp(string email)
        {
            return this.participants.Any(p => p.Email.ToUpper() == email.ToUpper());
        }

        public void Update(IEvent updatedEvent)
        {
            // Could potentially do some null-value checks here.
            this.Category = updatedEvent.Category;
            this.Name = updatedEvent.Name;
            this.Description = updatedEvent.Description;
            this.ScheduledDate = updatedEvent.ScheduledDate;
            this.Location = updatedEvent.Location;
        }

        public List<IParticipant> GetParticipants()
        {
            return this.participants;
        }
    }
}