using Application.CQRS.Queries.Conversations;
using Domain.Entities;
using MediatR;
using Repositories.Abstractions;

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