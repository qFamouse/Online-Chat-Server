using System.Net;
using Application.CQRS.Commands.Users;
using Configurations;
using Domain.Entities;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Application.CQRS.CommandHandlers.Users;

internal class SignUpUserCommandHandler : IRequestHandler<SignUpUserCommand, User>
{
    private readonly UserManager<User> _userManager;
    private readonly IdentityConfiguration _identityConfiguration;

    public SignUpUserCommandHandler
    (
        UserManager<User> userManager,
        IOptions<IdentityConfiguration> identityConfiguration
    )
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _identityConfiguration = identityConfiguration.Value ?? throw new ArgumentNullException(nameof(identityConfiguration));
    }

    public async Task<User> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
    {

        IdentityResult result = await _userManager.CreateAsync(request.User, request.Password);

        if (!result.Succeeded)
        {
            throw new ProblemDetailsException((int)HttpStatusCode.BadRequest, result.Errors.First().Description);
        }

        result = await _userManager.AddToRoleAsync(request.User, _identityConfiguration.DefaultRole);

        if (!result.Succeeded)
        {
            throw new ProblemDetailsException((int)HttpStatusCode.InternalServerError, result.Errors.First().Description);
        }

        return request.User;
    }
}