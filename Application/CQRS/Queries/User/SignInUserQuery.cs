using Contracts.Requests.User;
using Contracts.Views;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.User
{
    public class SignInUserQuery : IRequest<UserAuthorizationView>
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
