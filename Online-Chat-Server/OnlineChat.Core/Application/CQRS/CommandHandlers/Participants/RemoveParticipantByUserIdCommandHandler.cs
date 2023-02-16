using Application.CQRS.Commands.Participants;
using MediatR;
using Repositories.Abstractions;

namespace Application.CQRS.CommandHandlers.Participants;

internal class RemoveParticipantByUserIdCommandHandler : IRequestHandler<RemoveParticipantByUserIdCommand, Unit>
{
    private readonly IParticipantRepository _participantRepository;

    public RemoveParticipantByUserIdCommandHandler
    (
        IParticipantRepository participantRepository
    )
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