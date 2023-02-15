using Contracts.Requests.DirectMessage;
using MediatR;

namespace Application.CQRS.Commands.DirectMessage;

public class SendDirectMessageByReceiverIdCommand : IRequest<Data.Entities.DirectMessage>
{
    public int ReceiverId { get; set; }
    public string Message { get; set; }
    public int? TimeToLive { get; set; }

    public SendDirectMessageByReceiverIdCommand(SendDirectMessageByReceiverIdRequest request)
    {
        ReceiverId = request.ReceiverId;
        Message = request.Message;
        TimeToLive = request.TimeToLive;
    }
}