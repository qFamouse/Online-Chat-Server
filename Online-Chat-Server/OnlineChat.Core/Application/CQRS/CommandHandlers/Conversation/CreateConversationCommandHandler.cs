using Application.CQRS.Commands.Conversation;
using Application.Interfaces.Repositories;
using MediatR;
using Services.Interfaces;

namespace Application.CQRS.CommandHandlers.Conversation;

internal class CreateConversationCommandHandler : IRequestHandler<CreateConversationCommand, Data.Entities.Conversation>
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

    public async Task<Data.Entities.Conversation> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
    {
        var conversation = new Data.Entities.Conversation()
        {
            Title = request.Title,
            OwnerId = _identityService.GetUserId()
        };

        await _conversationRepository.InsertAsync(conversation, cancellationToken);
        await _conversationRepository.Save(cancellationToken);

        return conversation;
    }
}