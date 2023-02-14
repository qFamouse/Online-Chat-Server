using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.Queries.DirectMessage;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;

namespace Application.Validators.Queries.DirectMessage
{
    public class GetDirectMessageStatisticsQueryValidator : AbstractValidator<GetDirectMessageStatisticsQuery>
    {
        private readonly IIdentityService _identityService;
        private readonly UserManager<Entities.User> _userManager;


        public GetDirectMessageStatisticsQueryValidator
        (
            IIdentityService identityService,
            UserManager<Entities.User> userManager
        )
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
}
