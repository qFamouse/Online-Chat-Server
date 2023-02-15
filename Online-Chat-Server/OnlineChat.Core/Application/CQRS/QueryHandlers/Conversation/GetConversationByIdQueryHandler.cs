using Application.CQRS.Queries.Conversation;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.CQRS.QueryHandlers.Conversation;

internal class GetConversationByIdQueryHandler : IRequestHandler<GetConversationByIdQuery, Data.Entities.Conversation>
{
    private readonly IConversationRepository _conversationRepository;

    public GetConversationByIdQueryHandler
    (
        IConversationRepository conversationRepository
    )
    {
        _conversationRepository = conversationRepository ?? throw new ArgumentNullException(nameof(conversationRepository));
    }

    public async Task<Data.Entities.Conversation> Handle(GetConversationByIdQuery request, CancellationToken cancellationToken)
    {
        return await _conversationRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}