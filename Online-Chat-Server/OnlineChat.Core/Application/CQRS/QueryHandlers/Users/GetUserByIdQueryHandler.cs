using Application.CQRS.Queries.Users;
using Application.Mappers.Abstractions;
using Contracts.Views.User;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CQRS.QueryHandlers.Users;

internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserView>
{
    private readonly UserManager<User> _userManager;
    private readonly IUserMapper _userMapper;

    public GetUserByIdQueryHandler
    (
        UserManager<User> userManager, 
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