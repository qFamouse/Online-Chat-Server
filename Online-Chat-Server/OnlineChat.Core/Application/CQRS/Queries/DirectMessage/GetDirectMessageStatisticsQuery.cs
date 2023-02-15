using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Functions;
using MediatR;

namespace Application.CQRS.Queries.DirectMessage;

public class GetDirectMessageStatisticsQuery : IRequest<Stream>
{
    public GetDirectMessageStatisticsQuery() { }
}