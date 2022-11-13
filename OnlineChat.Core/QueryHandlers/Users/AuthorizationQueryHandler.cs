using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineChat.Core.Configurations;
using OnlineChat.Core.Entities;
using OnlineChat.Core.Exceptions;
using OnlineChat.Core.Interfaces.Services;
using OnlineChat.Core.Queries.Users;
using OnlineChat.Core.Views;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Core.QueryHandlers.Users
{
    public class AuthorizationQueryHandler : IRequestHandler<AuthorizationQuery, UserAuthorizationResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IdentityConfiguration _identityConfiguration;

        public AuthorizationQueryHandler(
            UserManager<User> userManager,
            IOptions<IdentityConfiguration> identityConfiguration)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _identityConfiguration = identityConfiguration.Value ?? throw new ArgumentNullException(nameof(identityConfiguration));
        }

        public async Task<UserAuthorizationResult> Handle(AuthorizationQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user is null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "User not found");
            }

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "Incorrect password");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)); // Claim all user roles

            List<Claim> authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.UserName),
            };
            authClaims.AddRange(roleClaims); // Adding user roles to main claims list

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_identityConfiguration.SecurityKey));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(_identityConfiguration.ExpiresHours),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserAuthorizationResult(encodedJwt, token.ValidTo);
        }
    }
}
