using Contracts.Requests.User;
using MediatR;


namespace Application.CQRS.Commands.User;

public class SignUpUserCommand : IRequest<Data.Entities.User>
{
    public Data.Entities.User User { get; set; }
    public string Password { get; set; }

    public SignUpUserCommand(UserSignupRequest request)
    {
        User = new Data.Entities.User()
        {
            UserName = request.UserName,
            Email = request.Email
        };

        Password = request.Password;
    }
}