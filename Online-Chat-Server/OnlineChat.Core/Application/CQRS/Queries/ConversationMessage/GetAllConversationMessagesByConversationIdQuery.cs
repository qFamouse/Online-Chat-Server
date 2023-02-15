using MediatR;

namespace Application.CQRS.Queries.ConversationMessage;

public class GetAllConversationMessagesByConversationIdQuery : IRequest<IEnumerable<Data.Entities.ConversationMessage>>
{
    public int ConversationId { get; set; }

    public GetAllConversationMessagesByConversationIdQuery(int conversationId)
    {
        ConversationId = conversationId;
    }
}