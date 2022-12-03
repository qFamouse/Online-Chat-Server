using MediatR;

namespace Application.CQRS.CommandHandlers.DirectMessage
{
    using Application.CQRS.Commands.DirectMessage;
    using Application.Entities;
    using Interfaces.Repositories;
    using Services.Interfaces;
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
