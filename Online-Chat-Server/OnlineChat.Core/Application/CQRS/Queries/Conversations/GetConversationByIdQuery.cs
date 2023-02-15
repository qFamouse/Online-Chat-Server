using Data.Entities;
using MediatR;

namespace Application.CQRS.Queries.Conversations;

public class GetConversationByIdQuery : IRequest<Conversation>
{
    public int Id { get; set; }

    public GetConversationByIdQuery(int id)
    {
        Id = id;
    }
}