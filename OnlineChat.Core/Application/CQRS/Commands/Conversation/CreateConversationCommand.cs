using Contracts.Requests.Conversation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.Conversation
{
    public class CreateConversationCommand : IRequest<Entities.Conversation>
    {
        public string Title { get; set; }
        // TODO: Add logotype (optional)

        public CreateConversationCommand(CreateConversationRequest request)
        {
            Title = request.Title;
        }
    }
}
