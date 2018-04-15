namespace Events
{
    using Events.Models;

    public static class Helpers
    {
        public static IEvent GetEvent(dynamic data)
        {
            var @event = new Event()
            {
                Id = data?.id,
                Category = data?.category,
                Description = data?.description,
                Name = data?.name,
                ScheduledDate = data?.scheduledDate,
                Location = new Address()
                {
                    City = data?.location?.city,
                    Country = data?.location?.country,
                    Latitude = data?.location?.latitude,
                    Longitude = data?.location?.longitude,
                    PostalCode = data?.location?.postalCode,
                    State = data?.location?.state
                }
            };
            return @event;
        }
    }
}
