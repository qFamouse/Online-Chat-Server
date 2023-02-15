using Contracts.Requests.ConversationMessage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.ConversationMessage;

public class SendConversationMessageByConversationIdCommand : IRequest<Data.Entities.ConversationMessage>
{
    public int ConversationId { get; set; }
    public string Text { get; set; }
    // TODO: Add attachment

    public SendConversationMessageByConversationIdCommand(SendConversationMessageByConversationIdRequest request)
    {
        ConversationId = request.ConversationId;
        Text = request.Text;
    }
}