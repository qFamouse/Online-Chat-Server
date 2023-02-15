using MediatR;

namespace Application.CQRS.Queries.Conversation;

public class GetConversationByIdQuery : IRequest<Data.Entities.Conversation>
{
    public int Id { get; set; }

    public GetConversationByIdQuery(int id)
    {
        Id = id;
    }
}