using Application.CQRS.Queries.Users;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.Validators.Queries.Users
{
    public class SignInTfaUserValidator : AbstractValidator<SignInTfaUserQuery>
    {
        private readonly UserManager<User> _userManager;

        public SignInTfaUserValidator(UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            BuildValidation();
        }

        private void BuildValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(UserMustBeExists);

            RuleFor(x => x.Code)
                .NotEmpty()
                .LessThan(1000000)
                .GreaterThan(99999);
        }

        private async Task<bool> UserMustBeExists(string email, CancellationToken cancellationToken)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
    }
}
