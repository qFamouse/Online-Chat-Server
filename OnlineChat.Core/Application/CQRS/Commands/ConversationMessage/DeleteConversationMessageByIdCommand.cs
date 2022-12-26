using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.ConversationMessage
{
    public class DeleteConversationMessageByIdCommand : IRequest<Entities.ConversationMessage>
    {
        public int Id { get; set; }

        public DeleteConversationMessageByIdCommand(int id)
        {
            Id = id;
        }
    }
}
