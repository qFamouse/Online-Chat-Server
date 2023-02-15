using Contracts.Requests.Conversation;
using MediatR;

namespace Application.CQRS.Commands.Conversation;

public class CreateConversationCommand : IRequest<Data.Entities.Conversation>
{
    public string Title { get; set; }
    // TODO: Add logotype (optional)

    public CreateConversationCommand(CreateConversationRequest request)
    {
        Title = request.Title;
    }
}