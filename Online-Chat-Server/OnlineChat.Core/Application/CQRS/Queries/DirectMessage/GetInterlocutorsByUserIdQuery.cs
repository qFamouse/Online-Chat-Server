using Contracts.Views.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.DirectMessage;

public class GetInterlocutorsByUserIdQuery : IRequest<IEnumerable<UserInterlocutorView>>
{
    public GetInterlocutorsByUserIdQuery() { }
}