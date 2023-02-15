using MediatR;

namespace Application.CQRS.Commands.ConversationMessage;

public class DeleteConversationMessageByIdCommand : IRequest<Data.Entities.ConversationMessage>
{
    public int Id { get; set; }

    public DeleteConversationMessageByIdCommand(int id)
    {
        Id = id;
    }
}