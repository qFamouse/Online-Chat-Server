using Application.CQRS.Commands.ConversationMessage;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.CQRS.CommandHandlers.ConversationMessage;

internal class DeleteConversationMessageByIdCommandHandler : IRequestHandler<DeleteConversationMessageByIdCommand, Data.Entities.ConversationMessage>
{
    private readonly IConversationMessagesRepository _conversationMessagesRepository;

    public DeleteConversationMessageByIdCommandHandler
    (
        IConversationMessagesRepository conversationMessagesRepository
    )
    {
        _conversationMessagesRepository = conversationMessagesRepository ?? throw new ArgumentNullException(nameof(conversationMessagesRepository));
    }

    public Task<Data.Entities.ConversationMessage> Handle(DeleteConversationMessageByIdCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}