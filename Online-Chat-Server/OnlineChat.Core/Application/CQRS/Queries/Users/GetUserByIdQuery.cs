using Contracts.Views.User;
using MediatR;

namespace Application.CQRS.Queries.Users;

public class GetUserByIdQuery : IRequest<UserView>
{
    public int Id { get; set; }
    public GetUserByIdQuery(int id)
    {
        Id = id;
    }
}