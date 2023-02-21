using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NuGet.Clerk.Abstractions;
using NuGet.Clerk.Models;

namespace NuGet.Clerk.Core;

public class ClerkDocumentService : IClerkDocumentService
{
    private readonly ClerkConfigurations _clerkConfigurations;
    private readonly HttpClient _httpClient;

    public ClerkDocumentService
    (
        IOptions<ClerkConfigurations> clerkOptions,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _clerkConfigurations = clerkOptions.Value ?? throw new ArgumentNullException(nameof(clerkOptions));

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(_clerkConfigurations.BaseAddress),
            DefaultRequestHeaders =
            {
                Authorization = new AuthenticationHeaderValue(
                    "Bearer", 
                    httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", ""))
            }
        };
    }

    public async Task<Stream> GeneratePdfByUsageStatisticsAsync(UsageStatistics statistics)
    {
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"Documents?TotalMessages={statistics.TotalMessages}&TotalSent={statistics.TotalSent}&TotalReceived={statistics.TotalReceived}");

        var response = await _httpClient.SendAsync(request);
        var stream = await response.Content.ReadAsStreamAsync();

        return stream;
    }
}