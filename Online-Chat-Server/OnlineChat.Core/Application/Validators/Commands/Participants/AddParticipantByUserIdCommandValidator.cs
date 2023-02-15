using Application.CQRS.Commands.Participants;
using Application.Interfaces.Repositories;
using Data.Entities;
using Data.Queries;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Resources.Messages;
using Services.Interfaces;

namespace Application.Validators.Commands.Participants;

internal sealed class AddParticipantByUserIdCommandValidator : AbstractValidator<AddParticipantByUserIdCommand>
{
    private readonly IParticipantRepository _participantRepository;
    private readonly IConversationRepository _conversationRepository;
    private readonly IIdentityService _identityService;
    private readonly UserManager<User> _userManager;

    public AddParticipantByUserIdCommandValidator(IConversationRepository conversationRepository, IParticipantRepository participantRepository, IIdentityService identityService, UserManager<User> userManager)
    {
        _participantRepository = participantRepository ?? throw new ArgumentNullException(nameof(participantRepository));
        _conversationRepository = conversationRepository ?? throw new ArgumentNullException(nameof(conversationRepository));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

        RuleFor(x => x.UserId)
            .NotEmpty()
            .MustAsync(async (id, CancellationToken) => await _userManager.FindByIdAsync(id.ToString()) != null).WithMessage(BaseMessages.NotFound);


        RuleFor(x => x)
            .MustAsync(MustNotBeParticipantOfConversation).WithMessage(BaseMessages.AlreadyExists);

        RuleFor(x => x.ConversationId)
            .NotEmpty()
            .MustAsync(_conversationRepository.ExistsAsync).WithMessage(BaseMessages.NotFound)
            .MustAsync(MustBeParticipantOfConversation).WithMessage(BaseMessages.AccessDenied);
    }

    private async Task<bool> MustBeParticipantOfConversation(int conversationId, CancellationToken cancellationToken)
    {
        var currentUserId = _identityService.GetUserId();
        var participant = await _participantRepository.GetByQueryAsync(new ParticipantQuery()
        {
            ConversationId = conversationId,
            UserId = currentUserId
        });

        return participant != null;
    }

    private async Task<bool> MustNotBeParticipantOfConversation(AddParticipantByUserIdCommand command, CancellationToken cancellationToken)
    {
        var participant = await _participantRepository.GetByQueryAsync(new ParticipantQuery()
        {
            ConversationId = command.ConversationId,
            UserId = command.UserId
        });

        return participant == null;
    }
}