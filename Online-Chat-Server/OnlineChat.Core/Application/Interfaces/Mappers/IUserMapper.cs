using Contracts.Views.User;
using Data.Entities;
using Mapster;

namespace Application.Interfaces.Mappers;

[Mapper]
public interface IUserMapper
{
    AboutUserView MapToAbout(User user);
    UserView MapToView(User user);
}