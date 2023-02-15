using Application.CQRS.Queries.User;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Resources;

namespace Application.Validators.Commands.User;

internal class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    private readonly UserManager<Data.Entities.User> _userManager;
    public GetUserByIdQueryValidator(UserManager<Data.Entities.User> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MustAsync(async (id, CancellationToken) => await _userManager.FindByIdAsync(id.ToString()) != null).WithMessage(Messages.NotFound);
    }
}