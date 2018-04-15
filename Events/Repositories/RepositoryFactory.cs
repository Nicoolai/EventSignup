namespace Events.Repositories
{
    /// <summary>
    /// Static factory to encapsulate the repositories.
    /// Primarily to make sure I have persistent data when debugging and testing.
    /// </summary>
    static class RepositoryFactory
    {
        private static IEventsRepository eventsRepository;
        private static IParticipantsRepository participantsRepository;

        public static IEventsRepository GetEventsRepository()
        {
            if (eventsRepository == null)
            {
                eventsRepository = new EventsRepository();
            }
            return eventsRepository;
        }

        public static IParticipantsRepository GetParticipantsRepository()
        {
            if (participantsRepository == null)
            {
                participantsRepository = new ParticipantsRepository();
            }
            return participantsRepository;
        }
    }
}
