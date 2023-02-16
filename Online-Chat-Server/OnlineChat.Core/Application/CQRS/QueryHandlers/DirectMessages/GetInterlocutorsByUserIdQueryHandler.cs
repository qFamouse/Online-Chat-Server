using Application.CQRS.Queries.DirectMessages;
using Contracts.Views.User;
using Domain.Entities;
using MediatR;
using Repositories.Abstractions;
using Services.Abstractions;

namespace Application.CQRS.QueryHandlers.DirectMessages;

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
        IEnumerable<User> interlocutors = await _directMessageRepository.GetInterlocutorsByUserIdAsync(userId, cancellationToken);

        var interlocutorsView = interlocutors.Select(i => new UserInterlocutorView()
        {
            Id = i.Id,
            UserName = i.UserName,
        });

        return interlocutorsView;
    }
}