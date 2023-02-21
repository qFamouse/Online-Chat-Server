using Application.CQRS.Queries.Documents;
using Application.Documents;
using MediatR;
using NuGet.Clerk.Models;
using QuestPDF.Fluent;

namespace Application.CQRS.QueryHandlers.Documents;

internal class GenerateDocumentByUsageStatisticsQueryHandler : IRequestHandler<GenerateDocumentByUsageStatisticsQuery, Stream>
{
    public Task<Stream> Handle(GenerateDocumentByUsageStatisticsQuery request, CancellationToken cancellationToken)
    {
        var usageStatistics = new UsageStatistics
        {
            TotalMessages = request.TotalMessages,
            TotalSent = request.TotalSent,
            TotalReceived = request.TotalReceived
        };

        var document = new UsageStatisticsDocument(usageStatistics);

        Stream stream = new MemoryStream();
        document.GeneratePdf(stream);

        stream.Seek(0, SeekOrigin.Begin);
        return Task.FromResult(stream);
    }
}