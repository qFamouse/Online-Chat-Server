using Contracts.Requests.User;
using Contracts.Views;
using MediatR;

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
