using Microsoft.Extensions.Options;
using NuGet.Clerk.Abstractions;
using NuGet.Clerk.Models;

namespace NuGet.Clerk.Core;

public class ClerkDocumentService : IClerkDocumentService
{
    private readonly ClerkConfigurations _clerkConfigurations;

    public ClerkDocumentService
    (
        IOptions<ClerkConfigurations> clerkOptions
    )
    {
        _clerkConfigurations = clerkOptions.Value ?? throw new ArgumentNullException(nameof(clerkOptions));
    }

    public async Task<Stream> GeneratePdfByUsageStatisticsAsync(UsageStatistics statistics)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_clerkConfigurations.BaseAddress);

        var request = new HttpRequestMessage(HttpMethod.Get,
            $"Documents?TotalMessages={statistics.TotalMessages}&TotalSent={statistics.TotalSent}&TotalReceived={statistics.TotalReceived}");

        var response = await httpClient.SendAsync(request);
        var stream = await response.Content.ReadAsStreamAsync();

        return stream;
    }
}