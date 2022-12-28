using Application.CQRS.Queries.DirectMessage;
using Application.Interfaces.Repositories;
using MediatR;
using Services.Interfaces;

namespace Application.CQRS.QueryHandlers.DirectMessage
{
    internal class GetDirectChatByReceiverIdQueryHandler :
        IRequestHandler<GetDirectChatByReceiverIdQuery, IEnumerable<Entities.DirectMessage>>
    {
        private readonly IDirectMessageRepository _directMessageRepository;
        private readonly IIdentityService _identityService;

        public GetDirectChatByReceiverIdQueryHandler(IDirectMessageRepository directMessageRepository, IIdentityService identityService)
        {
            _directMessageRepository = directMessageRepository ?? throw new ArgumentNullException(nameof(directMessageRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<IEnumerable<Entities.DirectMessage>> Handle(GetDirectChatByReceiverIdQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Entities.DirectMessage> messages = await _directMessageRepository.GetDirectMessagesByUsersId(_identityService.GetUserId(), request.ReceiverId);
            return messages;
        }
    }
}
