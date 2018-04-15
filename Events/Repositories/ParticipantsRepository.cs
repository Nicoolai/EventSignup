using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Models;

namespace Events.Repositories
{
    class ParticipantsRepository : IParticipantsRepository
    {
        public List<IEvent> GetEvents(IParticipant participant)
        {
            throw new NotImplementedException();
        }

        public List<IEvent> GetEvents(string email)
        {
            throw new NotImplementedException();
        }

        public List<IEvent> RemoveParticipant(IParticipant participant)
        {
            throw new NotImplementedException();
        }

        public List<IEvent> RemoveParticipant(string email)
        {
            throw new NotImplementedException();
        }
    }
}
