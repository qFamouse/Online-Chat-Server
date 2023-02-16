using Contracts.Requests.ConversationMessage;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Commands.ConversationMessages;

public class SendConversationMessageByConversationIdCommand : IRequest<ConversationMessage>
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