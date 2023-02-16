using Domain.Entities;
using MediatR;

namespace Application.CQRS.Queries.ConversationMessages;

public class GetAllConversationMessagesByConversationIdQuery : IRequest<IEnumerable<ConversationMessage>>
{
    public int ConversationId { get; set; }

    public GetAllConversationMessagesByConversationIdQuery(int conversationId)
    {
        ConversationId = conversationId;
    }
}