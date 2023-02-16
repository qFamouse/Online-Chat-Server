using Application.CQRS.Commands.Conversations;
using FluentValidation;
using Repositories.Abstractions;
using Resources.Messages;
using Services.Abstractions;

namespace Application.Validators.Commands.Conversations;

public sealed class DeleteConversationByIdCommandValidator : AbstractValidator<DeleteConversationByIdCommand>
{
    private readonly IConversationRepository _conversationRepository;
    private readonly IIdentityService _identityService;

    public DeleteConversationByIdCommandValidator(IConversationRepository conversationRepository, IIdentityService identityService)
    {
        _conversationRepository = conversationRepository ?? throw new ArgumentNullException(nameof(conversationRepository));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));

        BuildValidation();
    }

    private void BuildValidation()
    {
        RuleFor(x => x.ConversationId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MustAsync(_conversationRepository.ExistsAsync).WithMessage(BaseMessages.NotFound)
            .MustAsync(MustBeConversationOwner).WithMessage(BaseMessages.AccessDenied);
    }

    private async Task<bool> MustBeConversationOwner(int conversationId, CancellationToken cancellationToken)
    {
        var currentUserId = _identityService.GetUserId();
        var conversation = await _conversationRepository.GetByIdAsync(conversationId, cancellationToken);
        return conversation.OwnerId == currentUserId;
    }
}