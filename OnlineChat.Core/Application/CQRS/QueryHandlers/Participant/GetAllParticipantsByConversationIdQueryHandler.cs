using Application.CQRS.Queries.Participant;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.QueryHandlers.Participant
{
    internal class GetAllParticipantsByConversationIdQueryHandler : IRequestHandler<GetAllParticipantsByConversationIdQuery, IEnumerable<Entities.Participant>>
    {
        private readonly IParticipantRepository _participantRepository;

        public GetAllParticipantsByConversationIdQueryHandler(IParticipantRepository participantRepository)
        {
            _participantRepository = participantRepository ?? throw new ArgumentNullException(nameof(participantRepository));
        }

        public Task<IEnumerable<Entities.Participant>> Handle(GetAllParticipantsByConversationIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
