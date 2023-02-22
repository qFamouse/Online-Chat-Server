using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using NuGet.Clerk.Abstractions;
using NuGet.Clerk.Models;

namespace NuGet.Clerk.Core;

public class ClerkDocumentService : IClerkDocumentService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AuthenticationHeaderValue? _authenticationHeaderValue;

    public ClerkDocumentService
    (
        IHttpContextAccessor httpContextAccessor,
        IHttpClientFactory httpClientFactory
    )
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));

        var authorization = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Split(" ");
        _authenticationHeaderValue = authorization.Length == 2
            ? new AuthenticationHeaderValue(authorization[0], authorization[1])
            : default;
    }

    public async Task<Stream> GeneratePdfByUsageStatisticsAsync(UsageStatistics statistics)
    {
        var client = _httpClientFactory.CreateClient("clerk");
        client.DefaultRequestHeaders.Authorization = _authenticationHeaderValue;

        var request = new HttpRequestMessage(HttpMethod.Get,
            $"Documents?TotalMessages={statistics.TotalMessages}&TotalSent={statistics.TotalSent}&TotalReceived={statistics.TotalReceived}");

        var response = await client.SendAsync(request);
        var stream = await response.Content.ReadAsStreamAsync();

        return stream;
    }
}