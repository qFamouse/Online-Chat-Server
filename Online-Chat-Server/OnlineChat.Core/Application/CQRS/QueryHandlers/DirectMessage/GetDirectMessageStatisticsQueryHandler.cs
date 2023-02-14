using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.Queries.DirectMessage;
using Application.Functions;
using Application.Interfaces.Repositories;
using MediatR;
using Services.Interfaces;

namespace Application.CQRS.QueryHandlers.DirectMessage
{
    internal class GetDirectMessageStatisticsQueryHandler : IRequestHandler<GetDirectMessageStatisticsQuery , DirectMessageStatistics>
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

        public async Task<DirectMessageStatistics> Handle(GetDirectMessageStatisticsQuery request, CancellationToken cancellationToken)
        {
            int userId = _identityService.GetUserId();
            var statistics = await _directMessageRepository.GetDirectMessageStatisticsByUserIdAsync(userId, cancellationToken);

            return statistics;
        }
    }
}
