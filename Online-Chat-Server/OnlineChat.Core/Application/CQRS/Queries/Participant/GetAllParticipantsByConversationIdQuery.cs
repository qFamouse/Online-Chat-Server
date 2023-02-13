using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.Participant
{
    public class GetAllParticipantsByConversationIdQuery : IRequest<IEnumerable<Entities.Participant>>
    {
        public int ConversationId { get; set; }

        public GetAllParticipantsByConversationIdQuery(int conversationId)
        {
            ConversationId = conversationId;
        }
    }
}
