using Application.CQRS.Queries.DirectMessage;
using Application.Interfaces.Repositories;
using Contracts.Views;
using MediatR;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.QueryHandlers.DirectMessage
{
    internal class GetInterlocutorsByUserIdQueryHandler : IRequestHandler<GetInterlocutorsByUserIdQuery, IEnumerable<UserInterlocutorView>>
    {
        private readonly IDirectMessageRepository _directMessageRepository;
        private readonly IIdentityService _identityService;

        public GetInterlocutorsByUserIdQueryHandler
        (
            IDirectMessageRepository directMessageRepository, 
            IIdentityService identityService
        )
        {
            _directMessageRepository = directMessageRepository ?? throw new ArgumentNullException(nameof(directMessageRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<IEnumerable<UserInterlocutorView>> Handle(GetInterlocutorsByUserIdQuery request, CancellationToken cancellationToken)
        {
            int userId = _identityService.GetUserId();
            IEnumerable<Entities.User> interlocutors = await _directMessageRepository.GetInterlocutorsByUserIdAsync(userId, cancellationToken);

            var interlocutorsView = interlocutors.Select(i => new UserInterlocutorView()
            {
                Id = i.Id,
                UserName = i.UserName,
            });

            return interlocutorsView;
        }
    }
}
