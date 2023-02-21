using Application.CQRS.Queries.DirectMessages;
using MediatR;
using NuGet.Clerk.Abstractions;
using NuGet.Clerk.Models;
using Repositories.Abstractions;
using Services.Abstractions;

namespace Application.CQRS.QueryHandlers.DirectMessages;

internal class GetDirectMessageStatisticsQueryHandler : IRequestHandler<GetDirectMessageStatisticsQuery , Stream>
{
    private readonly IDirectMessageRepository _directMessageRepository;
    private readonly IIdentityService _identityService;
    private readonly IClerkDocumentService _clerkDocumentService;

    public GetDirectMessageStatisticsQueryHandler
    (
        IDirectMessageRepository directMessageRepository,
        IIdentityService identityService,
        IClerkDocumentService clerkDocumentService
    )
    {
        _directMessageRepository = directMessageRepository ?? throw new ArgumentNullException(nameof(directMessageRepository));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _clerkDocumentService = clerkDocumentService ?? throw new ArgumentNullException(nameof(clerkDocumentService));
    }

    public async Task<Stream> Handle(GetDirectMessageStatisticsQuery request, CancellationToken cancellationToken)
    {
        int userId = _identityService.GetUserId();
        var statistics = await _directMessageRepository.GetDirectMessageStatisticsByUserIdAsync(userId, cancellationToken);

        var statisticsMap = new UsageStatistics
        {
            TotalMessages = statistics.TotalMessages,
            TotalSent = statistics.TotalSent,
            TotalReceived = statistics.TotalReceived
        };

        var stream = await _clerkDocumentService.GeneratePdfByUsageStatisticsAsync(statisticsMap);

        return stream;
    }
}