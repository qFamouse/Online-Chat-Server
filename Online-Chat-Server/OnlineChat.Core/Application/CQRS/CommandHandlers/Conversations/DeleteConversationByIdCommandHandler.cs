using Application.CQRS.Commands.Conversations;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.CQRS.CommandHandlers.Conversations;

internal class DeleteConversationByIdCommandHandler : IRequestHandler<DeleteConversationByIdCommand, Unit>
{
    private readonly IConversationRepository _conversationRepository;

    public DeleteConversationByIdCommandHandler
    (
        IConversationRepository conversationRepository
    )
    {
        _conversationRepository = conversationRepository ?? throw new ArgumentNullException(nameof(conversationRepository));
    }

    public async Task<Unit> Handle(DeleteConversationByIdCommand request, CancellationToken cancellationToken)
    {
        await _conversationRepository.DeleteByIdAsync(request.ConversationId, cancellationToken);
        await _conversationRepository.Save(cancellationToken);

        return Unit.Value;
    }
}