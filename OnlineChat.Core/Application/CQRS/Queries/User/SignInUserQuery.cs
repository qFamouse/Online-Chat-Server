using Contracts.Requests.User;
using Contracts.Views;
using MediatR;

namespace Application.CQRS.Queries.User
{
    public class SignInUserQuery : IRequest<UserAuthorizationView>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public SignInUserQuery(UserAuthorizationRequest request)
        {
            Email = request.Email;
            Password = request.Password;
        }
    }
}
