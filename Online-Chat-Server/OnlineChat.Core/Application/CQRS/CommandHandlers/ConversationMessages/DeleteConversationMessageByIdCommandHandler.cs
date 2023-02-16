using Application.CQRS.Commands.ConversationMessages;
using Data.Entities;
using MediatR;
using Repositories.Abstractions;

namespace Application.CQRS.CommandHandlers.ConversationMessages;

internal class DeleteConversationMessageByIdCommandHandler : IRequestHandler<DeleteConversationMessageByIdCommand, ConversationMessage>
{
    private readonly IConversationMessagesRepository _conversationMessagesRepository;

    public DeleteConversationMessageByIdCommandHandler
    (
        IConversationMessagesRepository conversationMessagesRepository
    )
    {
        _conversationMessagesRepository = conversationMessagesRepository ?? throw new ArgumentNullException(nameof(conversationMessagesRepository));
    }

    public Task<ConversationMessage> Handle(DeleteConversationMessageByIdCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}