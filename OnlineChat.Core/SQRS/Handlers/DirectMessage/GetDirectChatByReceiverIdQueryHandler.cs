using MediatR;
using OnlineChat.Core.Interfaces.Repositories;
using OnlineChat.Core.SQRS.Queries.DirectMessage;
using OnlineChat.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Core.SQRS.Handlers.DirectMessage
{
    using OnlineChat.Core.Entities;
    using OnlineChat.Core.Interfaces.Services;

    public class GetDirectChatByReceiverIdQueryHandler :
        IRequestHandler<GetDirectChatByReceiverIdQuery, IEnumerable<DirectMessage>>
    {
        private readonly IDirectMessageRepository _directMessageRepository;
        private readonly IIdentityService _identityService;

        public GetDirectChatByReceiverIdQueryHandler(IDirectMessageRepository directMessageRepository, IIdentityService identityService)
        {
            _directMessageRepository = directMessageRepository ?? throw new ArgumentNullException(nameof(directMessageRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<IEnumerable<DirectMessage>> Handle(GetDirectChatByReceiverIdQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<DirectMessage> messages = await _directMessageRepository.GetDirectMessagesByUsersId(_identityService.Id, request.ReceiverId);
            return messages;
        }
    }
}
