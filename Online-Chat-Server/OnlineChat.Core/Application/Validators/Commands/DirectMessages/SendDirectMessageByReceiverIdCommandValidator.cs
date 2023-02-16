using Application.CQRS.Commands.DirectMessages;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Repositories.Abstractions;
using Resources.Messages;
using Services.Abstractions;

namespace Application.Validators.Commands.DirectMessages;

public sealed class SendDirectMessageByReceiverIdCommandValidator : AbstractValidator<SendDirectMessageByReceiverIdCommand>
{
    private readonly UserManager<User> _userManager;

    public SendDirectMessageByReceiverIdCommandValidator(UserManager<User> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

        BuildValidation();;
    }

    private void BuildValidation()
    {
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