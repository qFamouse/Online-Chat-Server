using Application.CQRS.Commands.Participant;
using Application.Interfaces.Repositories;
using Application.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.CommandHandlers.Participant
{
    internal class RemoveParticipantByUserIdCommandHandler : IRequestHandler<RemoveParticipantByUserIdCommand, Unit>
    {
        private readonly IParticipantRepository _participantRepository;

        public RemoveParticipantByUserIdCommandHandler(IParticipantRepository participantRepository)
        {
            _participantRepository = participantRepository ?? throw new ArgumentNullException(nameof(participantRepository));
        }

        public async Task<Unit> Handle(RemoveParticipantByUserIdCommand request, CancellationToken cancellationToken)
        {
            await _participantRepository.DeleteByIdAsync(request.ConversationId, cancellationToken);
            await _participantRepository.Save(cancellationToken);

            return Unit.Value;
        }
    }
}
