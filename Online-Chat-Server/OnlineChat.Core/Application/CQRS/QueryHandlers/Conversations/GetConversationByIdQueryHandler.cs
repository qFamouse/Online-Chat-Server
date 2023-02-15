using Application.CQRS.Queries.Conversations;
using Application.Interfaces.Repositories;
using Data.Entities;
using MediatR;

namespace Application.CQRS.QueryHandlers.Conversations;

internal class GetConversationByIdQueryHandler : IRequestHandler<GetConversationByIdQuery, Conversation>
{
    private readonly IConversationRepository _conversationRepository;

    public GetConversationByIdQueryHandler
    (
        IConversationRepository conversationRepository
    )
    {
        _conversationRepository = conversationRepository ?? throw new ArgumentNullException(nameof(conversationRepository));
    }

    public async Task<Conversation> Handle(GetConversationByIdQuery request, CancellationToken cancellationToken)
    {
        return await _conversationRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}