using Application.CQRS.Queries.Users;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Resources.Messages;
using Services.Abstractions;

namespace Application.Validators.Queries.Users;

public sealed class AboutUserQueryValidator : AbstractValidator<AboutUserQuery>
{
    private readonly IIdentityService _identityService;
    private readonly UserManager<User> _userManager;

    public AboutUserQueryValidator(IIdentityService identityService, UserManager<User> userManager)
    {
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

        BuildValidation();
    }

    private void BuildValidation()
    {
        RuleFor(x => _identityService.GetUserId())
            .NotEmpty()
            .MustAsync(MustBeExists).WithMessage(UserMessages.UserNotFound);
    }

    private async Task<bool> MustBeExists(int userId, CancellationToken cancellationToken)
    {
        return await _userManager.FindByIdAsync(userId.ToString()) != null;
    }
}