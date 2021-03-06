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

    public static class UpdateEventFunction
    {
        [FunctionName("UpdateEvent")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Admin, "put", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            try
            {
                log.Info("C# HTTP trigger function processed a request.");

                // Get request body
                dynamic data = await req.Content.ReadAsAsync<object>();
                IEvent updatedEvent = Helpers.GetEvent(data);

                var eventsRepository = RepositoryFactory.GetEventsRepository();

                // Get existing event, if it exists
                var existingEvent = eventsRepository.GetEvent(updatedEvent.Id);
                if (existingEvent == null)
                {
                    return req.CreateErrorResponse(HttpStatusCode.NotFound, "Event not found.");
                }

                // Copy values over 
                existingEvent.Update(updatedEvent);

                return req.CreateResponse(HttpStatusCode.OK, existingEvent);
            }
            catch (Exception ex)
            {
                return req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
