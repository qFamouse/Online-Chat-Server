using MediatR;

namespace Application.CQRS.Commands.Conversations;

public class DeleteConversationByIdCommand : IRequest<Unit>
{
    public int ConversationId { get; set; }

    public DeleteConversationByIdCommand(int id)
    {
        ConversationId = id;
    }
}