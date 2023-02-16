using Application.CQRS.Queries.DirectMessages;
using Application.Documents;
using MediatR;
using QuestPDF.Fluent;
using Repositories.Abstractions;
using Services.Interfaces;

namespace Application.CQRS.QueryHandlers.DirectMessages;

internal class GetDirectMessageStatisticsQueryHandler : IRequestHandler<GetDirectMessageStatisticsQuery , Stream>
{
    private readonly IDirectMessageRepository _directMessageRepository;
    private readonly IIdentityService _identityService;

    public GetDirectMessageStatisticsQueryHandler
    (
        IDirectMessageRepository directMessageRepository,
        IIdentityService identityService
    )
    {
        _directMessageRepository = directMessageRepository ?? throw new ArgumentNullException(nameof(directMessageRepository));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    }

    public async Task<Stream> Handle(GetDirectMessageStatisticsQuery request, CancellationToken cancellationToken)
    {
        int userId = _identityService.GetUserId();
        var statistics = await _directMessageRepository.GetDirectMessageStatisticsByUserIdAsync(userId, cancellationToken);

        var document = new DirectMessageStatisticsDocument(statistics);

        Stream stream = new MemoryStream();
        document.GeneratePdf(stream);
        stream.Seek(0, SeekOrigin.Begin);
        return stream;
    }
}