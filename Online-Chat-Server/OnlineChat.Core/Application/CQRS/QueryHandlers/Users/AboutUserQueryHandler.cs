using Application.CQRS.Queries.Users;
using Application.Interfaces.Mappers;
using Contracts.Views.User;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;

namespace Application.CQRS.QueryHandlers.Users;

internal class AboutUserQueryHandler : IRequestHandler<AboutUserQuery, AboutUserView>
{
    private readonly UserManager<User> _userManager;
    private readonly IIdentityService _identityService;
    private readonly IUserMapper _userMapper;

    public AboutUserQueryHandler
    (
        UserManager<User> userManager, 
        IIdentityService identityService, 
        IUserMapper userMapper
    )
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
    }

    public async Task<AboutUserView> Handle(AboutUserQuery request, CancellationToken cancellationToken)
    {
        int userId = _identityService.GetUserId();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        return _userMapper.MapToAbout(user);
    }
}