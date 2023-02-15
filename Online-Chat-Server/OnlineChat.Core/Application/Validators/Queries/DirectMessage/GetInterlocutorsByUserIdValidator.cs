using Application.CQRS.Queries.DirectMessage;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;

namespace Application.Validators.Queries.DirectMessage;

internal class GetInterlocutorsByUserIdValidator : AbstractValidator<GetInterlocutorsByUserIdQuery>
{
    private readonly IIdentityService _identityService;
    private readonly UserManager<Data.Entities.User> _userManager;

    public GetInterlocutorsByUserIdValidator(IIdentityService identityService, UserManager<Data.Entities.User> userManager)
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