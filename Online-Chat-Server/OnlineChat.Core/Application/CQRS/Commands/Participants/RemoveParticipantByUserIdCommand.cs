using Contracts.Requests.Participant;
using MediatR;

namespace Application.CQRS.Commands.Participants;

public class RemoveParticipantByUserIdCommand : IRequest<Unit>
{
    public int ConversationId { get; set; }
    public int UserId { get; set; }

    public RemoveParticipantByUserIdCommand(RemoveParticipantByUserIdRequest request)
    {
        ConversationId = request.ConversationId;
        UserId = request.UserId;
    }
}