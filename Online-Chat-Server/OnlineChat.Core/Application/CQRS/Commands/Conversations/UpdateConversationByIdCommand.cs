using Contracts.Requests.Conversation;
using Data.Entities;
using MediatR;

namespace Application.CQRS.Commands.Conversations;

public class UpdateConversationByIdCommand : IRequest<Conversation>
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