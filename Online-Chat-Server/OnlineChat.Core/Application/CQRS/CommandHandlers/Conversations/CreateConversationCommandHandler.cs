using Application.CQRS.Commands.Conversations;
using Domain.Entities;
using MediatR;
using Repositories.Abstractions;
using Services.Abstractions;

namespace Application.CQRS.CommandHandlers.Conversations;

internal class CreateConversationCommandHandler : IRequestHandler<CreateConversationCommand, Conversation>
{
    private readonly IConversationRepository _conversationRepository;
    private readonly IIdentityService _identityService;

    public CreateConversationCommandHandler
    (
        IConversationRepository conversationRepository, 
        IIdentityService identityService
    )
    {
        _conversationRepository = conversationRepository ?? throw new ArgumentNullException(nameof(conversationRepository));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    }

    public async Task<Conversation> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
    {
        var conversation = new Conversation()
        {
            Title = request.Title,
            OwnerId = _identityService.GetUserId()
        };

        await _conversationRepository.InsertAsync(conversation, cancellationToken);
        await _conversationRepository.Save(cancellationToken);

        return conversation;
    }
}