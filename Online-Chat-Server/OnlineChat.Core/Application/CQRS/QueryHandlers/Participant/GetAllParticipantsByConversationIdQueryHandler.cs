using Application.CQRS.Queries.Participant;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.CQRS.QueryHandlers.Participant;

internal class GetAllParticipantsByConversationIdQueryHandler : IRequestHandler<GetAllParticipantsByConversationIdQuery, IEnumerable<Data.Entities.Participant>>
{
    private readonly IParticipantRepository _participantRepository;

    public GetAllParticipantsByConversationIdQueryHandler
    (
        IParticipantRepository participantRepository
    )
    {
        _participantRepository = participantRepository ?? throw new ArgumentNullException(nameof(participantRepository));
    }

    public Task<IEnumerable<Data.Entities.Participant>> Handle(GetAllParticipantsByConversationIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}