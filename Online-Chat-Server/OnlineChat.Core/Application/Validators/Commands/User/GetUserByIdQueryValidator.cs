using Application.CQRS.Queries.User;
using Application.CQRS.QueryHandlers.User;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Commands.User
{
    internal class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        private readonly UserManager<Entities.User> _userManager;
        public GetUserByIdQueryValidator(UserManager<Entities.User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .MustAsync(async (id, CancellationToken) => await _userManager.FindByIdAsync(id.ToString()) != null).WithMessage(Messages.NotFound);
        }
    }
}
