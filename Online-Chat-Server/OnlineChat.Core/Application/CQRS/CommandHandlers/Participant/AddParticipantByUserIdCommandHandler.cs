using Application.CQRS.Commands.Participant;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.CQRS.CommandHandlers.Participant;

internal class AddParticipantByUserIdCommandHandler : IRequestHandler<AddParticipantByUserIdCommand, Data.Entities.Participant>
{
    private readonly IParticipantRepository _participantRepository;

    public AddParticipantByUserIdCommandHandler
    (
        IParticipantRepository participantRepository
    )
    {
        _participantRepository = participantRepository ?? throw new ArgumentNullException(nameof(participantRepository));
    }

    public async Task<Data.Entities.Participant> Handle(AddParticipantByUserIdCommand request, CancellationToken cancellationToken)
    {
        var participant = new Data.Entities.Participant()
        {
            ConversationId = request.ConversationId,
            UserId = request.UserId
        };

        await _participantRepository.InsertAsync(participant, cancellationToken);
        await _participantRepository.Save(cancellationToken);

        return participant;
    }
}