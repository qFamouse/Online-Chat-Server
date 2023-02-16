using Application.CQRS.Queries.Participants;
using Domain.Entities;
using MediatR;
using Repositories.Abstractions;

namespace Application.CQRS.QueryHandlers.Participants;

internal class GetAllParticipantsByConversationIdQueryHandler : IRequestHandler<GetAllParticipantsByConversationIdQuery, IEnumerable<Participant>>
{
    private readonly IParticipantRepository _participantRepository;

    public GetAllParticipantsByConversationIdQueryHandler
    (
        IParticipantRepository participantRepository
    )
    {
        _participantRepository = participantRepository ?? throw new ArgumentNullException(nameof(participantRepository));
    }

    public Task<IEnumerable<Participant>> Handle(GetAllParticipantsByConversationIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}