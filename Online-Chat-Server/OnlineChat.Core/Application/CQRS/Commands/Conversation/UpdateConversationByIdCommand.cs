using Contracts.Requests.Conversation;
using MediatR;

namespace Application.CQRS.Commands.Conversation;

public class UpdateConversationByIdCommand : IRequest<Data.Entities.Conversation>
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