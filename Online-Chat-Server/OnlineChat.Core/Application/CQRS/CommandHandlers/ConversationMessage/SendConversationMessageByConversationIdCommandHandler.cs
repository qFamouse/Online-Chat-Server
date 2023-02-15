using Application.CQRS.Commands.ConversationMessage;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.CQRS.CommandHandlers.ConversationMessage;

internal class SendConversationMessageByConversationIdCommandHandler : IRequestHandler<SendConversationMessageByConversationIdCommand, Data.Entities.ConversationMessage>
{
    private readonly IConversationMessagesRepository _conversationMessagesRepository;

    public SendConversationMessageByConversationIdCommandHandler
    (
        IConversationMessagesRepository conversationMessagesRepository
    )
    {
        _conversationMessagesRepository = conversationMessagesRepository ?? throw new ArgumentNullException(nameof(conversationMessagesRepository));
    }

    public Task<Data.Entities.ConversationMessage> Handle(SendConversationMessageByConversationIdCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}