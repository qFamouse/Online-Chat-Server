using MediatR;
using Microsoft.AspNetCore.Identity;
using OnlineChat.Core.Commands.Users;
using OnlineChat.Core.Entities;
using OnlineChat.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Core.CommandHandlers.Users
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, User>
    {
        private readonly UserManager<User> _userManager;

        public RegistrationCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<User> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(request.User, request.Password);

            if (!result.Succeeded)
            {
                // TODO: Make HttpStatusCodeException 
                // throw new HttpStatusCodeException(HttpStatusCode.BadRequest, result.Errors.First().Description);
                throw new Exception(result.Errors.First().Description);
            }

            // TODO: Create configuration for default user role - _identityConfiguration.UserRole
            result = await _userManager.AddToRoleAsync(request.User, "User");

            if (!result.Succeeded)
            {
                // TODO: Make HttpStatusCodeException 
                throw new Exception(result.Errors.First().Description);
            }

            return request.User;
        }
    }
}
