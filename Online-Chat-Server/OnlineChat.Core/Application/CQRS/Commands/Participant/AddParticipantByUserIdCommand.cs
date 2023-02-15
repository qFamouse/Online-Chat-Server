using Contracts.Requests.Participant;
using MediatR;

namespace Application.CQRS.Commands.Participant;

public class AddParticipantByUserIdCommand : IRequest<Data.Entities.Participant>
{
    public int ConversationId { get; set; }
    public int UserId { get; set; }

    public AddParticipantByUserIdCommand(AddParticipantByUserIdRequest request)
    {
        ConversationId = request.ConversationId;
        UserId = request.UserId;
    }
}