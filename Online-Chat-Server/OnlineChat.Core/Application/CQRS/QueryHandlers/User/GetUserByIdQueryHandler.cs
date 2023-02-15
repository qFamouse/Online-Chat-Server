using MediatR;
using Microsoft.AspNetCore.Identity;
using Application.CQRS.Queries.User;
using Application.Interfaces.Mappers;
using Contracts.Views.User;

namespace Application.CQRS.QueryHandlers.User;

internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserView>
{
    private readonly UserManager<Data.Entities.User> _userManager;
    private readonly IUserMapper _userMapper;

    public GetUserByIdQueryHandler
    (
        UserManager<Data.Entities.User> userManager, 
        IUserMapper userMapper
    )
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
    }

    public async Task<UserView> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());

        return _userMapper.MapToView(user);
    }
}