using MediatR;

namespace Application.CQRS.Queries.Participant;

public class GetAllParticipantsByConversationIdQuery : IRequest<IEnumerable<Data.Entities.Participant>>
{
    public int ConversationId { get; set; }

    public GetAllParticipantsByConversationIdQuery(int conversationId)
    {
        ConversationId = conversationId;
    }
}