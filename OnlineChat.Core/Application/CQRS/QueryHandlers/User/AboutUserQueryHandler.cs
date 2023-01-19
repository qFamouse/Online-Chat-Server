using Application.CQRS.Queries.User;
using Contracts.Views;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.QueryHandlers.User
{
    internal class AboutUserQueryHandler : IRequestHandler<AboutUserQuery, AboutUserView>
    {
        private readonly UserManager<Entities.User> _userManager;
        private readonly IIdentityService _identityService;

        public AboutUserQueryHandler(UserManager<Entities.User> userManager, IIdentityService identityService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<AboutUserView> Handle(AboutUserQuery request, CancellationToken cancellationToken)
        {
            int userId = _identityService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());

            return new AboutUserView()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
            };
        }
    }
}
