using Application.CQRS.Queries.DirectMessages;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;

namespace Application.Validators.Queries.DirectMessages;

internal class GetInterlocutorsByUserIdValidator : AbstractValidator<GetInterlocutorsByUserIdQuery>
{
    private readonly IIdentityService _identityService;
    private readonly UserManager<User> _userManager;

    public GetInterlocutorsByUserIdValidator(IIdentityService identityService, UserManager<User> userManager)
    {
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

        BuildValidation();
    }

    private void BuildValidation()
    {
        RuleFor(x => _identityService.GetUserId())
            .NotEmpty()
            .MustAsync(MustBeExists);
    }

    private async Task<bool> MustBeExists(int userId, CancellationToken cancellationToken)
    {
        return await _userManager.FindByIdAsync(userId.ToString()) != null;
    }
}