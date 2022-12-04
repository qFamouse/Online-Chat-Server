using MediatR;

namespace Application.CQRS.Queries.DirectMessage
{
    using Application.Entities;

    public class GetDirectChatByReceiverIdQuery : IRequest<IEnumerable<DirectMessage>>
    {
        public int ReceiverId { get; set; }

        public GetDirectChatByReceiverIdQuery(int receiverId)
        {
            ReceiverId = receiverId;
        }
    }
}
