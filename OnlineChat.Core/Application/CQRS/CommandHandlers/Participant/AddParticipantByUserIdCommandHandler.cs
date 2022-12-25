using Application.CQRS.Commands.Participant;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.CommandHandlers.Participant
{
    public class AddParticipantByUserIdCommandHandler : IRequestHandler<AddParticipantByUserIdCommand, Entities.Participant>
    {
        private readonly IParticipantRepository _participantRepository;

        public AddParticipantByUserIdCommandHandler(IParticipantRepository participantRepository)
        {
            _participantRepository = participantRepository ?? throw new ArgumentNullException(nameof(participantRepository));
        }

        public async Task<Entities.Participant> Handle(AddParticipantByUserIdCommand request, CancellationToken cancellationToken)
        {
            var participant = new Entities.Participant()
            {
                ConversationId = request.ConversationId,
                UserId = request.UsertId
            };

            await _participantRepository.InsertAsync(participant);
            await _participantRepository.Save();

            return participant;
        }
    }
}
