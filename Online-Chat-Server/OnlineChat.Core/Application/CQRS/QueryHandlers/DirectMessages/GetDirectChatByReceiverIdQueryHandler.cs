using Application.CQRS.Queries.DirectMessages;
using Application.Interfaces.Mappers;
using Application.Interfaces.Repositories;
using Contracts.Views;
using MediatR;
using Services.Interfaces;

namespace Application.CQRS.QueryHandlers.DirectMessages;

internal class GetDirectChatByReceiverIdQueryHandler : IRequestHandler<GetDirectChatByReceiverIdQuery, IEnumerable<ChatMessageDetailView>>
{
    private readonly IDirectMessageRepository _directMessageRepository;
    private readonly IIdentityService _identityService;
    private readonly IDirectMessageMapper _directMessageMapper;

    public GetDirectChatByReceiverIdQueryHandler
    (
        IDirectMessageRepository directMessageRepository,
        IIdentityService identityService,
        IDirectMessageMapper directMessageMapper
    )
    {
        _directMessageRepository = directMessageRepository ?? throw new ArgumentNullException(nameof(directMessageRepository));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _directMessageMapper = directMessageMapper ?? throw new ArgumentNullException(nameof(directMessageMapper));
    }

    public async Task<IEnumerable<ChatMessageDetailView>> Handle(GetDirectChatByReceiverIdQuery request, CancellationToken cancellationToken)
    {
        int currentUserId = _identityService.GetUserId();

        var directMessages = await _directMessageRepository
            .GetDetailDirectMessagesByUsersIdAsync(currentUserId, request.ReceiverId, cancellationToken);

        var messages = _directMessageMapper.MapToDetailView(directMessages);

        return messages;
    }
}