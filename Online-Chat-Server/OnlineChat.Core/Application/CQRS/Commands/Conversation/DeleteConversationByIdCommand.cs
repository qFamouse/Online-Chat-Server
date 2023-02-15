using MediatR;

namespace Application.CQRS.Commands.Conversation;

public class DeleteConversationByIdCommand : IRequest<Unit>
{
    public int ConversationId { get; set; }

    public DeleteConversationByIdCommand(int id)
    {
        ConversationId = id;
    }
}