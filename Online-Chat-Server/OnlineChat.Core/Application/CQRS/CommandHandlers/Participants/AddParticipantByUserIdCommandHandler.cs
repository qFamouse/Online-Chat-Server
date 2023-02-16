using Application.CQRS.Commands.Participants;
using Domain.Entities;
using MediatR;
using Repositories.Abstractions;

namespace Application.CQRS.CommandHandlers.Participants;

internal class AddParticipantByUserIdCommandHandler : IRequestHandler<AddParticipantByUserIdCommand, Participant>
{
    private readonly IParticipantRepository _participantRepository;

    public AddParticipantByUserIdCommandHandler
    (
        IParticipantRepository participantRepository
    )
    {
        _participantRepository = participantRepository ?? throw new ArgumentNullException(nameof(participantRepository));
    }

    public async Task<Participant> Handle(AddParticipantByUserIdCommand request, CancellationToken cancellationToken)
    {
        var participant = new Participant()
        {
            ConversationId = request.ConversationId,
            UserId = request.UserId
        };

        await _participantRepository.InsertAsync(participant, cancellationToken);
        await _participantRepository.Save(cancellationToken);

        return participant;
    }
}