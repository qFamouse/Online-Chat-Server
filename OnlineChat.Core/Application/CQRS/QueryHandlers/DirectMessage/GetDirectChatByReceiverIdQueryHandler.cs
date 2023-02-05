using Application.CQRS.Queries.DirectMessage;
using Application.Interfaces.Repositories;
using Contracts.Views;
using MediatR;
using Services.Interfaces;

namespace Application.CQRS.QueryHandlers.DirectMessage
{
    internal class GetDirectChatByReceiverIdQueryHandler :
        IRequestHandler<GetDirectChatByReceiverIdQuery, IEnumerable<ChatMessageDetailView>>
    {
        private readonly IDirectMessageRepository _directMessageRepository;
        private readonly IIdentityService _identityService;

        public GetDirectChatByReceiverIdQueryHandler(IDirectMessageRepository directMessageRepository, IIdentityService identityService)
        {
            _directMessageRepository = directMessageRepository ?? throw new ArgumentNullException(nameof(directMessageRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<IEnumerable<ChatMessageDetailView>> Handle(GetDirectChatByReceiverIdQuery request, CancellationToken cancellationToken)
        {
            int currentUserId = _identityService.GetUserId();

            var directMessages = await _directMessageRepository
                .GetDetailDirectMessagesByUsersIdAsync(currentUserId, request.ReceiverId, cancellationToken);

            var messages = directMessages.Select(dm => new ChatMessageDetailView()
            {
                Id = dm.Id,
                SenderId = dm.SenderId,
                Message = dm.Message,
                Time = dm.CreatedAt,
                Attachments = dm.Attachments.Select(a => new AttachmentView
                {
                    Id = a.Id,
                    OriginalName = a.OriginalName,
                    TimestampName = a.TimestampName,
                    Path = a.Path,
                    DirectMessageId = a.DirectMessageId,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt
                }).ToList()
            });

            return messages;
        }
    }
}
