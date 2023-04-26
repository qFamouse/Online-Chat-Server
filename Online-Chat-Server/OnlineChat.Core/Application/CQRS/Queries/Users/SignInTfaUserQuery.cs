using Contracts.Requests.User;
using Contracts.Views.User;
using MediatR;

namespace Application.CQRS.Queries.Users
{
    public class SignInTfaUserQuery : IRequest<UserAuthorizationTfaView>
    {
        public string Email { get; set; }
        public int Code { get; set; }

        public SignInTfaUserQuery(UserAuthorizationTfaRequest request)
        {
            Email = request.Email;
            Code = request.Code;
        }
    }
}
