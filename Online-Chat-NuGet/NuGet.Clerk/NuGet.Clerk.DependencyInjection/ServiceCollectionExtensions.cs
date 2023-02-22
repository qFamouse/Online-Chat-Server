using Microsoft.Extensions.DependencyInjection;
using NuGet.Clerk.Abstractions;
using NuGet.Clerk.Core;

namespace NuGet.Clerk.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddClerk(this IServiceCollection services, Action<HttpClient> configureClient)
    {
        services.AddHttpClient("clerk", configureClient);

        services.AddTransient<IClerkDocumentService, ClerkDocumentService>();
    }
}