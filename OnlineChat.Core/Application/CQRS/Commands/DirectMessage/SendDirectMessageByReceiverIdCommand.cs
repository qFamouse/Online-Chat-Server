using Contracts.Requests.DirectMessage;
using MediatR;

namespace Application.CQRS.Commands.DirectMessage
{
    public class SendDirectMessageByReceiverIdCommand : IRequest<Entities.DirectMessage>
    {
        public int ReceiverId { get; set; }
        public string Message { get; set; }
        public int? TimeToLife { get; set; }

        public SendDirectMessageByReceiverIdCommand(SendDirectMessageByReceiverIdRequest request)
        {
            ReceiverId = request.ReceiverId;
            Message = request.Message;
            TimeToLife = request.TimeToLife;
        }
    }
}
