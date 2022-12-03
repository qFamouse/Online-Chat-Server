using Contracts.Requests.DirectMessage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.DirectMessage
{
    public class SendDirectMessageByReceiverIdCommand : IRequest<Entities.DirectMessage>
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
