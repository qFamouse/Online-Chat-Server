using Application.CQRS.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;
using Application.Interfaces.Mappers;
using Contracts.Views.User;

namespace Application.CQRS.QueryHandlers.User;

internal class AboutUserQueryHandler : IRequestHandler<AboutUserQuery, AboutUserView>
{
    private readonly UserManager<Data.Entities.User> _userManager;
    private readonly IIdentityService _identityService;
    private readonly IUserMapper _userMapper;

    public AboutUserQueryHandler
    (
        UserManager<Data.Entities.User> userManager, 
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