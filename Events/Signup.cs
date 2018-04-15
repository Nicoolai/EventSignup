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

    public static class Signup
    {
        [FunctionName("Signup")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            try
            {
                // Get request body
                dynamic data = await req.Content.ReadAsAsync<object>();
                
                if (string.IsNullOrEmpty((string)data?.eventId))
                {
                    return req.CreateErrorResponse(HttpStatusCode.BadRequest, "Missing Event id.");
                }

                IEventsRepository eventsRepository = RepositoryFactory.GetEventsRepository();
                var eventToSignup = eventsRepository.GetEvent((Guid)data.eventId);
                if (eventToSignup == null)
                {
                    return req.CreateErrorResponse(HttpStatusCode.NotFound, "Event not found.");
                }

                var participant = new Participant()
                {
                    Name = data?.name,
                    Email = data?.email,
                    PhoneNumber = data?.phonenumber,
                    Address = new Address()
                    {
                        City = data?.address?.city,
                        Country = data?.address?.country,
                        PostalCode = data?.address?.postalCode,
                        State = data?.address?.state
                    }
                };

                if (!participant.isValid())
                {
                    return req.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid signup information.");
                }

                // Check if participant is already signed up.
                if (eventToSignup.IsParticipantSignedUp(participant.Email))
                {
                    return req.CreateErrorResponse(HttpStatusCode.BadRequest, "You are already signed up to this event.");
                }

                eventsRepository.Signup(eventToSignup, participant);

                return req.CreateResponse(HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                log.Error("Error", ex, "Signup.cs");
                return req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
