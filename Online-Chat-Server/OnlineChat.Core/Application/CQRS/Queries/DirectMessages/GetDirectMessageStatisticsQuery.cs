using MediatR;

namespace Application.CQRS.Queries.DirectMessages;

public class GetDirectMessageStatisticsQuery : IRequest<Stream>
{
    public GetDirectMessageStatisticsQuery() { }
}