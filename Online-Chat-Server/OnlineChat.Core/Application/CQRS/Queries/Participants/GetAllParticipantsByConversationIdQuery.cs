using Data.Entities;
using MediatR;

namespace Application.CQRS.Queries.Participants;

public class GetAllParticipantsByConversationIdQuery : IRequest<IEnumerable<Participant>>
{
    public int ConversationId { get; set; }

    public GetAllParticipantsByConversationIdQuery(int conversationId)
    {
        ConversationId = conversationId;
    }
}