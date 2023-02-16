using Application.CQRS.Commands.Conversations;
using Data.Entities;
using MediatR;
using Repositories.Abstractions;

namespace Application.CQRS.CommandHandlers.Conversations;

internal class UpdateConversationByIdCommandHandler : IRequestHandler<UpdateConversationByIdCommand, Conversation>
{
    private readonly IConversationRepository _conversationRepository;

    public UpdateConversationByIdCommandHandler
    (
        IConversationRepository conversationRepository
    )
    {
        _conversationRepository = conversationRepository ?? throw new ArgumentNullException(nameof(conversationRepository));
    }

    public async Task<Conversation> Handle(UpdateConversationByIdCommand request, CancellationToken cancellationToken)
    {
        var conversation = await _conversationRepository.GetByIdAsync(request.ConversationId, cancellationToken);

        conversation.Title = request.Title ?? conversation.Title;
        conversation.OwnerId = request.OwnerId ?? conversation.OwnerId;

        await _conversationRepository.Save(cancellationToken);

        return conversation;
    }
}