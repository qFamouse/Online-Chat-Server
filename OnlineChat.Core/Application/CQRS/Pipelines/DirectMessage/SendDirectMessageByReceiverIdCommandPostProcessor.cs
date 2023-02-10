using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.Commands.DirectMessage;
using Contracts;
using MassTransit;
using MediatR.Pipeline;
using OnlineChat.Cron.Contracts;

namespace Application.CQRS.Pipelines.DirectMessage
{
    public class SendDirectMessageByReceiverIdCommandPostProcessor : IRequestPostProcessor<SendDirectMessageByReceiverIdCommand, Entities.DirectMessage>
    {
        private readonly IBus _bus;

        public SendDirectMessageByReceiverIdCommandPostProcessor
        (
            IBus bus
        )
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task Process(SendDirectMessageByReceiverIdCommand request, Entities.DirectMessage response, CancellationToken cancellationToken)
        {
            if (!request.TimeToLive.HasValue)
            {
                return;
            }

            var autoDelete = new MessageAutoDelete()
            {
                MessageId = response.Id,
                Delay = TimeSpan.FromSeconds(request.TimeToLive.Value)
            };

            await _bus.Publish(autoDelete, cancellationToken);
        }
    }
}
