using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using Contracts.Views.User;
using Mapster;

namespace Application.Interfaces.Mappers
{
    [Mapper]
    public interface IUserMapper
    {
        AboutUserView MapToAbout(User user);
        UserView MapToView(User user);
    }
}
