using Application.CQRS.Commands.ConversationMessages;
using Data.Entities;
using MediatR;
using Repositories.Abstractions;

namespace Application.CQRS.CommandHandlers.ConversationMessages;

internal class SendConversationMessageByConversationIdCommandHandler : IRequestHandler<SendConversationMessageByConversationIdCommand, ConversationMessage>
{
    private readonly IConversationMessagesRepository _conversationMessagesRepository;

    public SendConversationMessageByConversationIdCommandHandler
    (
        IConversationMessagesRepository conversationMessagesRepository
    )
    {
        _conversationMessagesRepository = conversationMessagesRepository ?? throw new ArgumentNullException(nameof(conversationMessagesRepository));
    }

    public Task<ConversationMessage> Handle(SendConversationMessageByConversationIdCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}