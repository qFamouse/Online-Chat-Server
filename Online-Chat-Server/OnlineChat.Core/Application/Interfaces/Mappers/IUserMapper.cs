using Contracts.Views.User;
using Domain.Entities;
using Mapster;

namespace Application.Interfaces.Mappers;

[Mapper]
public interface IUserMapper
{
    AboutUserView MapToAbout(User user);
    UserView MapToView(User user);
}