using Application.CQRS.Commands.Conversation;
using Application.Interfaces.Repositories;
using MediatR;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.CommandHandlers.Conversation
{
    internal class CreateConversationCommandHandler : IRequestHandler<CreateConversationCommand, Entities.Conversation>
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IIdentityService _identityService;

        public CreateConversationCommandHandler(IConversationRepository conversationRepository, IIdentityService identityService)
        {
            _conversationRepository = conversationRepository ?? throw new ArgumentNullException(nameof(conversationRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<Entities.Conversation> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
        {
            var conversation = new Entities.Conversation()
            {
                Title = request.Title,
                OwnerId = _identityService.GetUserId()
            };

            await _conversationRepository.InsertAsync(conversation);
            await _conversationRepository.Save();

            return conversation;
        }
    }
}
