using Application.CQRS.Queries.User;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;

namespace Application.Validators.Queries.User;

public sealed class AboutUserQueryValidator : AbstractValidator<AboutUserQuery>
{
    private readonly IIdentityService _identityService;
    private readonly UserManager<Data.Entities.User> _userManager;

    public AboutUserQueryValidator(IIdentityService identityService, UserManager<Data.Entities.User> userManager)
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