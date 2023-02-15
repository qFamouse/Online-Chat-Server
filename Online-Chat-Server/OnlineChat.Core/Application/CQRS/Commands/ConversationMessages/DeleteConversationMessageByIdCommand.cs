using Data.Entities;
using MediatR;

namespace Application.CQRS.Commands.ConversationMessages;

public class DeleteConversationMessageByIdCommand : IRequest<ConversationMessage>
{
    public int Id { get; set; }

    public DeleteConversationMessageByIdCommand(int id)
    {
        Id = id;
    }
}