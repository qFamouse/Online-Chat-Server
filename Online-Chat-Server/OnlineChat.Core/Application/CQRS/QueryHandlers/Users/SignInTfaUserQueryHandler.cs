using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using Application.CQRS.Queries.Users;
using Configurations;
using Contracts.Views.User;
using Domain.Entities;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Resources.Messages;
using Services.Abstractions;

namespace Application.CQRS.QueryHandlers.Users
{
    public class SignInTfaUserQueryHandler : IRequestHandler<SignInTfaUserQuery, UserAuthorizationTfaView>
    {
        private readonly ITfaService _tfaService;
        private readonly UserManager<User> _userManager;
        private readonly IOptions<AuthenticationConfiguration> _authenticationOptions;
        private readonly IOptions<IdentityConfiguration> _identityOptions;

        public SignInTfaUserQueryHandler
        (
            ITfaService tfaService, UserManager<User> userManager,
            IOptions<AuthenticationConfiguration> authenticationOptions,
            IOptions<IdentityConfiguration> identityOptions
            )
        {
            _tfaService = tfaService ?? throw new ArgumentNullException(nameof(tfaService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _authenticationOptions =
                authenticationOptions ?? throw new ArgumentNullException(nameof(authenticationOptions));
            _identityOptions = identityOptions ?? throw new ArgumentNullException(nameof(identityOptions));
        }

        public async Task<UserAuthorizationTfaView> Handle(SignInTfaUserQuery request, CancellationToken cancellationToken)
        {
            bool valid = await _tfaService.AuthenticateAsync(request.Email, request.Code);

            if (!valid)
            {
                throw new ProblemDetailsException(400, TfaMessages.InvalidCode);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)); // Claim all user roles

            List<Claim> authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };
            authClaims.AddRange(roleClaims); // Adding user roles to main claims list

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationOptions.Value.SecurityKey));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(_identityOptions.Value.ExpiresHours),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserAuthorizationTfaView()
            {
                Token = encodedJwt
            };
        }
    }
}
