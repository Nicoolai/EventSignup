namespace Events
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Events.Repositories;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Host;

    public static class CancelSignupFunction
    {
        [FunctionName("CancelSignup")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            try
            {
                log.Info("C# HTTP trigger function processed a request.");

                // Get request body
                dynamic data = await req.Content.ReadAsAsync<object>();

                // Validate data
                if (string.IsNullOrEmpty(data?.eventId) || string.IsNullOrEmpty(data?.email))
                {
                    return req.CreateErrorResponse(HttpStatusCode.BadRequest, "Missing parameters.");
                }

                IEventsRepository eventsRepository = RepositoryFactory.GetEventsRepository();

                // check if person is actually signed up.
                var @event = eventsRepository.GetEvent((Guid)data?.eventId);
                if (@event == null || !@event.IsParticipantSignedUp((string)data?.email))
                {
                    return req.CreateErrorResponse(HttpStatusCode.NotFound, "You are not signed up for this event.");
                }

                // Proceed to remove participant from event
                if (eventsRepository.CancelSignup(@event, (string)data?.email))
                {
                    // Successfully cancelled participation
                    return req.CreateResponse(HttpStatusCode.Accepted);
                }

                return req.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to cancel participation.");
            }
            catch (Exception ex)
            {
                return req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
