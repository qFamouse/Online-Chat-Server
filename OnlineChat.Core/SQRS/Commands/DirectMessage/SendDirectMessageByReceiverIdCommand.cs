using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Core.SQRS.Commands.DirectMessage
{
    using OnlineChat.Core.Entities;
    using OnlineChat.Core.Requests.DirectMessage;

    public class SendDirectMessageByReceiverIdCommand : IRequest<DirectMessage>
    {
        public int ReceiverId { get; set; }
        public string Message { get; set; }

        public SendDirectMessageByReceiverIdCommand(SendDirectMessageByReceiverIdRequest request)
        {
            ReceiverId = request.ReceiverId;
            Message = request.Message;
        }
    }
}
