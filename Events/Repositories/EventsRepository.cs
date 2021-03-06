﻿namespace Events.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Events.Models;

    class EventsRepository : IEventsRepository
    {
        private List<IEvent> events;

        public EventsRepository()
        {
            this.events = new List<IEvent>()
            {
                new Event() { Name = "Horse show", Category = "animalshow", Description = "Just a test event", Location = null, ScheduledDate = DateTime.Now },
                new Event() { Name = "Cow show", Category = "animalshow", Description = "Just a test event", Location = null, ScheduledDate = DateTime.Now.AddDays(2) },
                new Event() { Name = "Sheep show", Category = "animalshow", Description = "Just a test event", Location = null, ScheduledDate = DateTime.Now.AddDays(10) },
                new Event() { Name = "Pig show", Category = "animalshow", Description = "Just a test event", Location = null, ScheduledDate = DateTime.Now.AddDays(31) },
                new Event() { Name = "Potato show", Category = "vegatableshow", Description = "Just a test event", Location = null, ScheduledDate = DateTime.Now.AddDays(7) },
                new Event() { Name = "Carrot show", Category = "vegatableshow", Description = "Just a test event", Location = null, ScheduledDate = DateTime.Now.AddDays(4) },
                new Event() { Name = "Star Wars show", Category = "movieshow", Description = "Just a test event", Location = null, ScheduledDate = DateTime.Now.AddDays(3) },
                new Event() { Name = "The 5th Element show", Category = "movieshow", Description = "Just a test event", Location = null, ScheduledDate = DateTime.Now.AddDays(9) },
                new Event() { Name = "Driver show", Category = "movieshow", Description = "Just a test event", Location = null, ScheduledDate = DateTime.Now.AddDays(7) }
            };
        }

        public Guid CreateEvent(IEvent @event)
        {
            this.events.Add(@event);
            return @event.Id;
        }

        public IEvent GetEvent(Guid id)
        {
            return this.events.SingleOrDefault(e => e.Id == id);
        }

        public List<IEvent> GetEvents(string category = null, IAddress location = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var filteredEvents = this.events.ToList(); // copy list
            
            // Apply various filtering, if relevant.
            if (!string.IsNullOrEmpty(category))
            {
                filteredEvents = filteredEvents.Where(e => e.Category.ToUpper() == category.ToUpper()).ToList();
            }

            if (location != null)
            {
                // Should really be based on lattitude and longtitude, and be near location, instead of at location.
                filteredEvents = filteredEvents.Where(e => e.Location == location).ToList();
            }

            if (fromDate.HasValue)
            {
                filteredEvents = filteredEvents.Where(e => e.ScheduledDate >= fromDate.Value).ToList();
            }

            if (toDate.HasValue)
            {
                filteredEvents = filteredEvents.Where(e => e.ScheduledDate <= toDate.Value).ToList();
            }

            return filteredEvents;
        }

        public bool Signup(IEvent @event, IParticipant participant)
        {
            return @event.AddParticipant(participant);
        }
    }
}
