using Application.CQRS.Commands.Participants;
using Application.Interfaces.Repositories;
using Data.Entities;
using Data.Queries;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Resources;
using Resources.Messages;
using Services.Interfaces;

namespace Application.Validators.Commands.Participants;

internal sealed class RemoveParticipantByUserIdCommandValidator : AbstractValidator<RemoveParticipantByUserIdCommand>
{

    private readonly IParticipantRepository _participantRepository;
    private readonly IConversationRepository _conversationRepository;
    private readonly IIdentityService _identityService;
    private readonly UserManager<User> _userManager;


    public RemoveParticipantByUserIdCommandValidator(IConversationRepository conversationRepository, IParticipantRepository participantRepository, IIdentityService identityService, UserManager<User> userManager)
    {
        _participantRepository = participantRepository ?? throw new ArgumentNullException(nameof(participantRepository));
        _conversationRepository = conversationRepository ?? throw new ArgumentNullException(nameof(conversationRepository));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

        RuleFor(x => x.UserId)
            .NotEmpty()
            .MustAsync(async (id, CancellationToken) => await _userManager.FindByIdAsync(id.ToString()) != null).WithMessage(BaseMessages.NotFound);

        RuleFor(x => x)
            .MustAsync(MustBeParticipantOfConversation).WithMessage(BaseMessages.NotFound);

        RuleFor(x => x.ConversationId)
            .NotEmpty()
            .MustAsync(_conversationRepository.ExistsAsync).WithMessage(BaseMessages.NotFound)
            .MustAsync(MustBeOwnerOfConversation).WithMessage(BaseMessages.AccessDenied);
    }

    private async Task<bool> MustBeParticipantOfConversation(RemoveParticipantByUserIdCommand command, CancellationToken cancellationToken)
    {
        var participant = await _participantRepository.GetByQueryAsync(new ParticipantQuery()
        {
            ConversationId = command.ConversationId,
            UserId = command.UserId
        });

        return participant != null;
    }

    private async Task<bool> MustBeOwnerOfConversation(int conversationId, CancellationToken cancellationToken)
    {
        var currentUserId = _identityService.GetUserId();
        var conversation = await _conversationRepository.GetByIdAsync(conversationId, cancellationToken);

        return conversation.OwnerId == currentUserId;
    }
}