using Contracts.Requests.User;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Commands.Users;

public class SignUpUserCommand : IRequest<User>
{
    public User User { get; set; }
    public string Password { get; set; }

    public SignUpUserCommand(UserSignupRequest request)
    {
        User = new User()
        {
            UserName = request.UserName,
            Email = request.Email
        };

        Password = request.Password;
    }
}