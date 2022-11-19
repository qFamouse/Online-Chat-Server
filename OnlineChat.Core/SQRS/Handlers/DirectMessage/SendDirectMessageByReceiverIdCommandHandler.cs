using MediatR;
using OnlineChat.Core.SQRS.Commands.DirectMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Core.SQRS.Handlers.DirectMessage
{
    using Microsoft.AspNetCore.Identity;
    using OnlineChat.Core.Entities;
    using OnlineChat.Core.Interfaces.Repositories;
    using OnlineChat.Core.Interfaces.Services;
    using System.Threading;

    internal class SendDirectMessageByReceiverIdCommandHandler : IRequestHandler<SendDirectMessageByReceiverIdCommand, DirectMessage>
    {
        // identity service
        private readonly IDirectMessageRepository _directMessageRepository;
        private readonly IIdentityService _identityService;

        public SendDirectMessageByReceiverIdCommandHandler(IDirectMessageRepository directMessageRepository, IIdentityService identityService)
        {
            _directMessageRepository = directMessageRepository ?? throw new ArgumentNullException(nameof(directMessageRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<DirectMessage> Handle(SendDirectMessageByReceiverIdCommand request, CancellationToken cancellationToken)
        {
            var directMessage = new DirectMessage()
            {
                SenderId = _identityService.Id,
                ReceiverId = request.ReceiverId,
                Message = request.Message,
            };

            directMessage = await _directMessageRepository.InsertAsync(directMessage);
            await _directMessageRepository.Save();

            return directMessage;
        }
    }
}
