using MediatR;
using OnlineChat.Core.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Core.SQRS.Queries.DirectMessage
{
    using OnlineChat.Core.Entities;

    public class GetDirectChatByReceiverIdQuery : IRequest<IEnumerable<DirectMessage>>
    {
        public int ReceiverId { get; set; }

        public GetDirectChatByReceiverIdQuery(int receiverId)
        {
            ReceiverId = receiverId;
        }
    }
}
