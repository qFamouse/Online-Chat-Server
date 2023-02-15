using Application.CQRS.Queries.Users;
using Data.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;

namespace Application.Validators.Queries.Users;

public sealed class AboutUserQueryValidator : AbstractValidator<AboutUserQuery>
{
    private readonly IIdentityService _identityService;
    private readonly UserManager<User> _userManager;

    public AboutUserQueryValidator(IIdentityService identityService, UserManager<User> userManager)
    {
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));


        RuleFor(x => _identityService.GetUserId())
            .NotEmpty()
            .MustAsync(MustBeExists);
    }

    private async Task<bool> MustBeExists(int userId, CancellationToken cancellationToken)
    {
        return await _userManager.FindByIdAsync(userId.ToString()) != null;
    }
}