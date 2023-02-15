using Application.CQRS.Queries.ConversationMessage;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.CQRS.QueryHandlers.ConversationMessage;

internal class GetAllConversationMessagesByConversationIdQueryHandler : IRequestHandler<GetAllConversationMessagesByConversationIdQuery, IEnumerable<Data.Entities.ConversationMessage>>
{
    private readonly IConversationMessagesRepository _conversationMessagesRepository;

    public GetAllConversationMessagesByConversationIdQueryHandler
    (
        IConversationMessagesRepository conversationMessagesRepository
    )
    {
        _conversationMessagesRepository = conversationMessagesRepository;
    }

    public Task<IEnumerable<Data.Entities.ConversationMessage>> Handle(GetAllConversationMessagesByConversationIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}