using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Requests.Participant
{
    public class RemoveParticipantByUserIdRequest
    {
        public int ConversationId { get; set; }
        public int UsertId { get; set; }
    }
}
