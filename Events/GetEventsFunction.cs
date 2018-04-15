namespace Events
{
    using System;
    using System.Collections.Generic;
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

                IEventsRepository eventsRepository = RepositoryFactory.GetEventsRepository();

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
