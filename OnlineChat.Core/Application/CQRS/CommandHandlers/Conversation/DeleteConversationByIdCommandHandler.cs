using Application.CQRS.Commands.Conversation;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.CQRS.CommandHandlers.Conversation
{
    public class DeleteConversationByIdCommandHandler : IRequestHandler<DeleteConversationByIdCommand, Unit>
    {
        private readonly IConversationRepository _conversationRepository;

        public DeleteConversationByIdCommandHandler(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository ?? throw new ArgumentNullException(nameof(conversationRepository));
        }

        public async Task<Unit> Handle(DeleteConversationByIdCommand request, CancellationToken cancellationToken)
        {
            await _conversationRepository.DeleteByIdAsync(request.ConversationId);
            await _conversationRepository.Save();

            return Unit.Value;
        }
    }
}
