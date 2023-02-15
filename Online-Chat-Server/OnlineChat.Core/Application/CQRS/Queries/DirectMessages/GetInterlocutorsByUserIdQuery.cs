using Contracts.Views.User;
using MediatR;

namespace Application.CQRS.Queries.DirectMessages;

public class GetInterlocutorsByUserIdQuery : IRequest<IEnumerable<UserInterlocutorView>>
{
    public GetInterlocutorsByUserIdQuery() { }
}