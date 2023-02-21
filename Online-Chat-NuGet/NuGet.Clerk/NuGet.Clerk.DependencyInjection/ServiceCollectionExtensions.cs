using Microsoft.Extensions.DependencyInjection;
using NuGet.Clerk.Abstractions;
using NuGet.Clerk.Core;
using NuGet.Clerk.Models;

namespace NuGet.Clerk.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddClerk(this IServiceCollection services, Action<ClerkConfigurations> configureOptions)
    {
        services.Configure<ClerkConfigurations>(configureOptions);

        services.AddTransient<IClerkDocumentService, ClerkDocumentService>();
    }
}