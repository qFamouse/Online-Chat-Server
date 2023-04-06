using System.IdentityModel.Tokens.Jwt;
using System.Net;
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

namespace Application.CQRS.QueryHandlers.Users;

internal class SignInUserQueryHandler : IRequestHandler<SignInUserQuery, UserAuthorizationView>
{
    private readonly UserManager<User> _userManager;
    private readonly IOptions<AuthenticationConfiguration> _authenticationOptions;
    private readonly IOptions<IdentityConfiguration> _identityOptions;

    public SignInUserQueryHandler
    (
        UserManager<User> userManager,
        IOptions<AuthenticationConfiguration> identityConfiguration, 
        IOptions<IdentityConfiguration> identityOptions
    )
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _authenticationOptions = identityConfiguration ?? throw new ArgumentNullException(nameof(identityConfiguration));
        _identityOptions = identityOptions ?? throw new ArgumentNullException(nameof(identityOptions));
    }

    public async Task<UserAuthorizationView> Handle(SignInUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            throw new ProblemDetailsException((int)HttpStatusCode.NotFound, UserMessages.UserNotFound);
        }

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new ProblemDetailsException((int)HttpStatusCode.BadRequest, UserMessages.IncorrectPassword);
        }

        if (user.TwoFactorEnabled)
        {
            return new UserAuthorizationView()
            {
                IsAuthSuccessful = true,
                IsTfaEnabled = true
            };
        }

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

        return new UserAuthorizationView()
        {
            IsAuthSuccessful = true,
            IsTfaEnabled = false,
            Token = encodedJwt
        };
    }
}