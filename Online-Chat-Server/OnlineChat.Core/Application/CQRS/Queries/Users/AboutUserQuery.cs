using Contracts.Views.User;
using MediatR;

namespace Application.CQRS.Queries.Users;

public class AboutUserQuery : IRequest<AboutUserView>
{
    public AboutUserQuery() { }
}