using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Application.CQRS.QueryHandlers.User
{
    using Application.CQRS.Queries.User;
    using Application.Entities;
    using Configurations;
    using Contracts.Views;
    using Exceptions;

    public class SignInUserQueryHandler : IRequestHandler<SignInUserQuery, UserAuthorizationView>
    {
        private readonly UserManager<User> _userManager;
        private readonly IdentityConfiguration _identityConfiguration;

        public SignInUserQueryHandler(
            UserManager<User> userManager,
            IOptions<IdentityConfiguration> identityConfiguration)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _identityConfiguration = identityConfiguration.Value ?? throw new ArgumentNullException(nameof(identityConfiguration));
        }

        public async Task<UserAuthorizationView> Handle(SignInUserQuery request, CancellationToken cancellationToken)
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

            return new UserAuthorizationView(encodedJwt, token.ValidTo);
        }
    }
}
