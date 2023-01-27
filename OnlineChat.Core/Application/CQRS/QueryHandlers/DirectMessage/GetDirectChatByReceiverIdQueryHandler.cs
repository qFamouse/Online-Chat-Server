using Application.CQRS.Queries.DirectMessage;
using Application.Interfaces.Repositories;
using Contracts.Views;
using MediatR;
using Services.Interfaces;

namespace Application.CQRS.QueryHandlers.DirectMessage
{
    internal class GetDirectChatByReceiverIdQueryHandler :
        IRequestHandler<GetDirectChatByReceiverIdQuery, IEnumerable<ChatMessageView>>
    {
        private readonly IDirectMessageRepository _directMessageRepository;
        private readonly IIdentityService _identityService;

        public GetDirectChatByReceiverIdQueryHandler(IDirectMessageRepository directMessageRepository, IIdentityService identityService)
        {
            _directMessageRepository = directMessageRepository ?? throw new ArgumentNullException(nameof(directMessageRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<IEnumerable<ChatMessageView>> Handle(GetDirectChatByReceiverIdQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Entities.DirectMessage> directMessages = await _directMessageRepository
                .GetDirectMessagesByUsersIdAsync(_identityService.GetUserId(), request.ReceiverId);

            var messages = directMessages.Select(x => new ChatMessageView()
            {
                Id = x.Id,
                SenderId = x.SenderId,
                Message = x.Message,
                Time = x.CreatedAt,
            });

            return messages;
        }
    }
}
