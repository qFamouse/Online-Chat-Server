﻿using Application.CQRS.Commands.Conversations;
using Application.Interfaces.Repositories;
using FluentValidation;
using Resources;
using Resources.Messages;
using Services.Interfaces;

namespace Application.Validators.Commands.Conversations;

public sealed class UpdateConversationByIdCommandValidator : AbstractValidator<UpdateConversationByIdCommand>
{
    private readonly IConversationRepository _conversationRepository;
    private readonly IIdentityService _identityService;

    public UpdateConversationByIdCommandValidator(IConversationRepository conversationRepository, IIdentityService identityService)
    {
        _conversationRepository = conversationRepository ?? throw new ArgumentNullException(nameof(conversationRepository));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));

        RuleFor(x => x.ConversationId)
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