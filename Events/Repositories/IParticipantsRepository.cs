using Events.Models;
using System.Collections.Generic;

namespace Events.Repositories
{
    /// <summary>
    /// Repository for participant focused actions.
    /// Potentially someone would like to know where their personal information has been stored or even have it removed.
    /// This interface enables that.
    /// </summary>
    interface IParticipantsRepository
    {
        /// <summary>
        /// Get all events a participant has signed up for.
        /// </summary>
        /// <param name="participant">Full datastructure for participant.</param>
        /// <returns>The list of events the participant has signed up for.</returns>
        List<IEvent> GetEvents(IParticipant participant);
        /// <summary>
        /// Get all events a participant has signed up for, based on only an email.
        /// </summary>
        /// <param name="email">Email used to signup.</param>
        /// <returns>The list of events the participant has signed up for</returns>
        List<IEvent> GetEvents(string email);
        /// <summary>
        /// Removes participant data from all events.
        /// </summary>
        /// <param name="participant">Full datastructure for participant.</param>
        /// <returns>List of events the person was removed from.</returns>
        List<IEvent> RemoveParticipant(IParticipant participant);
        /// <summary>
        /// Removes participant data from all events.
        /// </summary>
        /// <param name="email">Email used to signup.</param>
        /// <returns>List of events the person was removed from.</returns>
        List<IEvent> RemoveParticipant(string email);
    }
}
