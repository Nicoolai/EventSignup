namespace Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Events.Models;
    using Events.Repositories;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Host;

    public static class GetEventsFunction
    {
        [FunctionName("Events")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            try
            {
                log.Info("C# HTTP trigger function processed a request.");

                // parse query parameter
                string eventId = req.GetQueryNameValuePairs()
                    .FirstOrDefault(q => string.Compare(q.Key, "eventId", true) == 0)
                    .Value;

                IEventsRepository eventsRepository = RepositoryFactory.GetEventsRepository();

                if (!string.IsNullOrEmpty(eventId))
                {
                    // Get of a specific event
                    var guid = Guid.Parse(eventId);
                    var @event = eventsRepository.GetEvent(guid);
                    return @event != null ? req.CreateResponse(HttpStatusCode.Found, @event) : req.CreateErrorResponse(HttpStatusCode.NotFound, "Event not found");
                }
                

                // Get request body
                dynamic data = await req.Content.ReadAsAsync<object>();
                List<IEvent> events = eventsRepository.GetEvents((string)data?.category, (IAddress)data?.location, (DateTime?)data?.fromDate, (DateTime?)data?.toDate);

                return req.CreateResponse(HttpStatusCode.OK, events);
            }
            catch (Exception ex)
            {
                log.Error("Error", ex, "GetEvents.cs");
                return req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
