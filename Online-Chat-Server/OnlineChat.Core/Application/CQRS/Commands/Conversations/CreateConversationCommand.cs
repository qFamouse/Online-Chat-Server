using Contracts.Requests.Conversation;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Commands.Conversations;

public class CreateConversationCommand : IRequest<Conversation>
{
    public string Title { get; set; }
    // TODO: Add logotype (optional)

    public CreateConversationCommand(CreateConversationRequest request)
    {
        Title = request.Title;
    }
}