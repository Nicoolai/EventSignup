﻿namespace Events.Repositories
{
    using System;
    using System.Collections.Generic;
    using Events.Models;

    /// <summary>
    /// Repository for event focused actions.
    /// </summary>
    interface IEventsRepository
    {
        IEvent GetEvent(Guid id);

        /// <summary>
        /// Get events. Allows you to get all events, by passing null values, or filter based on key parameters.
        /// </summary>
        /// <param name="category">Category to filter for.</param>
        /// <param name="location">A partially filled out Address structure, to limit events to a certain area.</param>
        /// <param name="fromDate">The start date to use for date filtering.</param>
        /// <param name="toDate">The end date to use for date filtering.</param>
        /// <returns></returns>
        List<IEvent> GetEvents(string category = null, IAddress location = null, DateTime? fromDate = null, DateTime? toDate = null);
        Guid CreateEvent(IEvent @event);
    }
}
