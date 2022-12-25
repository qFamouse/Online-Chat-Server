using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.Conversation
{
    public class DeleteConversationByIdCommand : IRequest<Unit>
    {
        public int ConversationId { get; set; }

        public DeleteConversationByIdCommand(int id)
        {
            ConversationId = id;
        }
    }
}
