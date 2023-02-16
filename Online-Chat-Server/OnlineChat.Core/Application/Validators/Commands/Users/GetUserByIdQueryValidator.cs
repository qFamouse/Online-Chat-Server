using Application.CQRS.Queries.Users;
using Data.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Resources.Messages;

namespace Application.Validators.Commands.Users;

internal class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    private readonly UserManager<User> _userManager;
    public GetUserByIdQueryValidator(UserManager<User> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MustAsync(async (id, CancellationToken) => await _userManager.FindByIdAsync(id.ToString()) != null).WithMessage(BaseMessages.NotFound);
    }
}