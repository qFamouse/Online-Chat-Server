﻿using System.IdentityModel.Tokens.Jwt;
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
    private readonly IdentityConfiguration _identityConfiguration;

    public SignInUserQueryHandler
    (
        UserManager<User> userManager,
        IOptions<IdentityConfiguration> identityConfiguration
    )
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _identityConfiguration = identityConfiguration.Value ?? throw new ArgumentNullException(nameof(identityConfiguration));
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

        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)); // Claim all user roles

        List<Claim> authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
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