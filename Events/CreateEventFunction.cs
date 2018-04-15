namespace Events
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Events.Models;
    using Events.Repositories;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Host;

    public static class CreateEventFunction
    {
        [FunctionName("CreateEvent")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Admin, "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            try
            {
                log.Info("C# HTTP trigger function processed a request.");

                // Get request body
                dynamic data = await req.Content.ReadAsAsync<object>();
                IEvent @event = Helpers.GetEvent(data);

                var eventsRepository = RepositoryFactory.GetEventsRepository();
                var guid = eventsRepository.CreateEvent(@event);

                return req.CreateResponse(HttpStatusCode.Created, guid);
            }
            catch (Exception ex)
            {
                log.Error("Error", ex, "GetEvents.cs");
                return req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
