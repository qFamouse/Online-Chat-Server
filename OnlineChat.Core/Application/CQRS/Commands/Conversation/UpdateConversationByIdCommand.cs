using Contracts.Requests.Conversation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.Conversation
{
    public class UpdateConversationByIdCommand : IRequest<Entities.Conversation>
    {
        public int ConversationId { get; set; }
        public int? OwnerId { get; set; }
        public string? Title { get; set; }
        // TODO: Add logotype

        public UpdateConversationByIdCommand(int conversationId, UpdateConversationByIdRequest request)
        {
            ConversationId = conversationId;
            OwnerId = request.OwnerId;
            Title = request.Title;
        }
    }
}
