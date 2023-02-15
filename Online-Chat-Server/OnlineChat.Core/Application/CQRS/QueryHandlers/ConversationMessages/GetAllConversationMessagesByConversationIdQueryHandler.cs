using Application.CQRS.Queries.ConversationMessages;
using Application.Interfaces.Repositories;
using Data.Entities;
using MediatR;

namespace Application.CQRS.QueryHandlers.ConversationMessages;

internal class GetAllConversationMessagesByConversationIdQueryHandler : IRequestHandler<GetAllConversationMessagesByConversationIdQuery, IEnumerable<ConversationMessage>>
{
    private readonly IConversationMessagesRepository _conversationMessagesRepository;

    public GetAllConversationMessagesByConversationIdQueryHandler
    (
        IConversationMessagesRepository conversationMessagesRepository
    )
    {
        _conversationMessagesRepository = conversationMessagesRepository;
    }

    public Task<IEnumerable<ConversationMessage>> Handle(GetAllConversationMessagesByConversationIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}