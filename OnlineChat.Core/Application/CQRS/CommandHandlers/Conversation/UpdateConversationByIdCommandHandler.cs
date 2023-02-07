using Application.CQRS.Commands.Conversation;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.CommandHandlers.Conversation
{
    internal class UpdateConversationByIdCommandHandler : IRequestHandler<UpdateConversationByIdCommand, Entities.Conversation>
    {
        private readonly IConversationRepository _conversationRepository;

        public UpdateConversationByIdCommandHandler
        (
            IConversationRepository conversationRepository
        )
        {
            _conversationRepository = conversationRepository ?? throw new ArgumentNullException(nameof(conversationRepository));
        }

        public async Task<Entities.Conversation> Handle(UpdateConversationByIdCommand request, CancellationToken cancellationToken)
        {
            var conversation = await _conversationRepository.GetByIdAsync(request.ConversationId, cancellationToken);

            conversation.Title = request.Title ?? conversation.Title;
            conversation.OwnerId = request.OwnerId ?? conversation.OwnerId;

            await _conversationRepository.Save(cancellationToken);

            return conversation;
        }
    }
}
