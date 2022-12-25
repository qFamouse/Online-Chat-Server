using Contracts.Requests.Participant;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.Participant
{
    public class RemoveParticipantByUserIdCommand : IRequest<Unit>
    {
        public int ConversationId { get; set; }
        public int UsertId { get; set; }

        public RemoveParticipantByUserIdCommand(RemoveParticipantByUserIdRequest request)
        {
            ConversationId = request.ConversationId;
            UsertId = request.UsertId;
        }
    }
}
