using Contracts.Requests.Participant;
using Data.Entities;
using MediatR;

namespace Application.CQRS.Commands.Participants;

public class AddParticipantByUserIdCommand : IRequest<Participant>
{
    public int ConversationId { get; set; }
    public int UserId { get; set; }

    public AddParticipantByUserIdCommand(AddParticipantByUserIdRequest request)
    {
        ConversationId = request.ConversationId;
        UserId = request.UserId;
    }
}