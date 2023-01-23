using MediatR;

namespace Application.CQRS.Queries.DirectMessage
{
    using Application.Entities;
    using Contracts.Views;

    public class GetDirectChatByReceiverIdQuery : IRequest<IEnumerable<ChatMessageView>>
    {
        public int ReceiverId { get; set; }

        public GetDirectChatByReceiverIdQuery(int receiverId)
        {
            ReceiverId = receiverId;
        }
    }
}
