using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
