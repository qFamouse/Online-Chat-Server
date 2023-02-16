using Contracts.Requests.DirectMessage;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Commands.DirectMessages;

public class SendDirectMessageByReceiverIdCommand : IRequest<DirectMessage>
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