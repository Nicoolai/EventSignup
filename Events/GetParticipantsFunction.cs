namespace Events
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Events.Repositories;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Host;

    public static class GetParticipantsFunction
    {
        [FunctionName("GetParticipants")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Admin, "get", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // parse query parameter
            string eventId = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "event", true) == 0)
                .Value;

            if (string.IsNullOrEmpty(eventId))
            {
                return req.CreateErrorResponse(HttpStatusCode.BadRequest, "Please pass an eventId on the query string.");
            }

            var eventGuid = Guid.Parse(eventId);

            var eventsRepository = RepositoryFactory.GetEventsRepository();
            var @event = eventsRepository.GetEvent(eventGuid);

            if (@event == null)
            {
                return req.CreateErrorResponse(HttpStatusCode.NotFound, "Event not found.");
            }

            var participants = @event.GetParticipants();
            return req.CreateResponse(HttpStatusCode.OK, participants);
        }
    }
}
