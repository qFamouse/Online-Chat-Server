using Application.Interfaces.Mappers;
using Contracts.Views.User;
using Data.Entities;

namespace Application.Mappers
{
    public partial class UserMapper : IUserMapper
    {
        public AboutUserView MapToAbout(User p1)
        {
            return p1 == null ? null : new AboutUserView()
            {
                Id = p1.Id,
                UserName = p1.UserName,
                Email = p1.Email
            };
        }
        public UserView MapToView(User p2)
        {
            return p2 == null ? null : new UserView()
            {
                Id = p2.Id,
                UserName = p2.UserName,
                Email = p2.Email
            };
        }
    }
}