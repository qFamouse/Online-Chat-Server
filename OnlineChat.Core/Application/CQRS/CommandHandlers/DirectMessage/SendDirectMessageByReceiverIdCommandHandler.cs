using Application.CQRS.Commands.DirectMessage;
using Application.Interfaces.Repositories;
using MediatR;
using Services.Interfaces;

namespace Application.CQRS.CommandHandlers.DirectMessage
{
    internal class SendDirectMessageByReceiverIdCommandHandler : IRequestHandler<SendDirectMessageByReceiverIdCommand, Entities.DirectMessage>
    {
        private readonly IDirectMessageRepository _directMessageRepository;
        private readonly IIdentityService _identityService;

        public SendDirectMessageByReceiverIdCommandHandler
        (
            IDirectMessageRepository directMessageRepository, 
            IIdentityService identityService
        )
        {
            _directMessageRepository = directMessageRepository ?? throw new ArgumentNullException(nameof(directMessageRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<Entities.DirectMessage> Handle(SendDirectMessageByReceiverIdCommand request, CancellationToken cancellationToken)
        {
            var directMessage = new Entities.DirectMessage()
            {
                SenderId = _identityService.GetUserId(),
                ReceiverId = request.ReceiverId,
                Message = request.Message,
            };

            directMessage = await _directMessageRepository.InsertAsync(directMessage, cancellationToken);
            await _directMessageRepository.Save(cancellationToken);

            return directMessage;
        }
    }
}
