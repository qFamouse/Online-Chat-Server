using Contracts.Requests.Documents;
using MediatR;

namespace Application.CQRS.Queries.Documents;

public class GenerateDocumentByUsageStatisticsQuery : IRequest<Stream>
{
    public int TotalMessages { get; set; }
    public int TotalSent { get; set; }
    public int TotalReceived { get; set; }

    public GenerateDocumentByUsageStatisticsQuery(GenerateDocumentByUsageStatisticsRequest request)
    {
        TotalMessages = request.TotalMessages;
        TotalSent = request.TotalSent;
        TotalReceived = request.TotalReceived;
    }
}