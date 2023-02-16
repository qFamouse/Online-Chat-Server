using Contracts.Views.User;
using Domain.Entities;
using Mapster;

namespace Application.Mappers.Abstractions;

[Mapper]
public interface IUserMapper
{
    AboutUserView MapToAbout(User user);
    UserView MapToView(User user);
}