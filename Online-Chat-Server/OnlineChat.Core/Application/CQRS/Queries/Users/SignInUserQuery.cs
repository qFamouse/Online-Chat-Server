using Contracts.Requests.User;
using Contracts.Views.User;
using MediatR;

namespace Application.CQRS.Queries.Users;

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