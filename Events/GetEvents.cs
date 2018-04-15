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
namespace Events
{
    public static class GetEvents
    {
        [FunctionName("Events")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            IEventsRepository eventsRepository = new EventsRepository();

            //if (req.Method == HttpMethod.Get)
            //{
            //    // Simple get, return all events.
            //    List<IEvent> events = eventsRepository.GetEvents();

            //    return req.CreateResponse(HttpStatusCode.OK, events);
            //}
            // Get request body
            dynamic data = await req.Content.ReadAsAsync<object>();
            //name = data?.name;
            List<IEvent> events = eventsRepository.GetEvents((string)data?.category, (IAddress)data?.location, (DateTime?)data?.fromDate, (DateTime?)data?.toDate);

            return req.CreateResponse(HttpStatusCode.OK, events);

            //return name == null
            //    ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
            //    : req.CreateResponse(HttpStatusCode.OK, "Hello " + name);
            return req.CreateResponse(HttpStatusCode.OK, "lala");
        }
    }
}
