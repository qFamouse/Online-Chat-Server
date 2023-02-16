using Application.CQRS.Commands.DirectMessages;
using Data.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Repositories.Abstractions;
using Resources.Messages;
using Services.Interfaces;

namespace Application.Validators.Commands.DirectMessages;

public sealed class SendDirectMessageByReceiverIdCommandValidator : AbstractValidator<SendDirectMessageByReceiverIdCommand>
{
    private readonly IDirectMessageRepository _directMessageRepository;
    private readonly IIdentityService _identityService;
    private readonly UserManager<User> _userManager;

    public SendDirectMessageByReceiverIdCommandValidator(IDirectMessageRepository directMessageRepository, IIdentityService identityService, UserManager<User> userManager)
    {
        _directMessageRepository = directMessageRepository ?? throw new ArgumentNullException(nameof(directMessageRepository));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));


        RuleFor(x => x.ReceiverId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MustAsync(UserMustBeExists).WithMessage(UserMessages.UserNotFound);

        RuleFor(x => x.TimeToLive)
            .GreaterThan(5)
            .LessThan(60);
    }

    private async Task<bool> UserMustBeExists(int userId, CancellationToken cancellationToken)
    {
        return await _userManager.FindByIdAsync(userId.ToString()) != null;
    }
}