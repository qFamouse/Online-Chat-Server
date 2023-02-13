using Contracts.Requests.Participant;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.Participant
{
    public class AddParticipantByUserIdCommand : IRequest<Entities.Participant>
    {
        public int ConversationId { get; set; }
        public int UserId { get; set; }

        public AddParticipantByUserIdCommand(AddParticipantByUserIdRequest request)
        {
            ConversationId = request.ConversationId;
            UserId = request.UserId;
        }
    }
}
