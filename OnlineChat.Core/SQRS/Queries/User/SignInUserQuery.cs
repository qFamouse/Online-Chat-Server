using MediatR;
using OnlineChat.Core.Entities;
using OnlineChat.Core.Requests.User;
using OnlineChat.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Core.SQRS.Queries.User
{
    public class SignInUserQuery : IRequest<UserAuthorizationResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public SignInUserQuery(UserAuthorizationRequest request)
        {
            UserName = request.UserName;
            Password = request.Password;
        }
    }
}
