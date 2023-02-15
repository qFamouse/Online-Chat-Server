using Application.CQRS.Commands.DirectMessages;
using Data.Entities;
using MassTransit;
using MediatR.Pipeline;
using OnlineChat.MassTransit.Contracts;

namespace Application.CQRS.Pipelines.DirectMessages;

public class SendDirectMessageByReceiverIdCommandPostProcessor : IRequestPostProcessor<SendDirectMessageByReceiverIdCommand, DirectMessage>
{
    private readonly IBus _bus;

    public SendDirectMessageByReceiverIdCommandPostProcessor
    (
        IBus bus
    )
    {
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    public async Task Process(SendDirectMessageByReceiverIdCommand request, DirectMessage response, CancellationToken cancellationToken)
    {
        if (!request.TimeToLive.HasValue)
        {
            return;
        }

        var autoDelete = new DeleteMessageByDelayContract()
        {
            MessageId = response.Id,
            Delay = TimeSpan.FromSeconds(request.TimeToLive.Value)
        };

        await _bus.Publish(autoDelete, cancellationToken);
    }
}