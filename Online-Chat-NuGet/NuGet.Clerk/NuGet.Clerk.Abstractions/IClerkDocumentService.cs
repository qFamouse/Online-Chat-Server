using NuGet.Clerk.Models;

namespace NuGet.Clerk.Abstractions;

public interface IClerkDocumentService
{
    Task<Stream> GeneratePdfByUsageStatisticsAsync(UsageStatistics statistics);
}