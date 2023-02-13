using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Hangfire;
using MassTransit;
using Microsoft.Extensions.Logging;
using OnlineChat.MassTransit.Contracts;

namespace Application.Consumers
{
    public class DeleteMessageByDelayConsumer : IConsumer<DeleteMessageByDelayContract>
    {
        private readonly ILogger<DeleteMessageByDelayConsumer> _logger;
        private readonly IMessageRepository _messageRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IBus _bus;

        public DeleteMessageByDelayConsumer
        (
            ILogger<DeleteMessageByDelayConsumer> logger,
            IMessageRepository messageRepository,
            IBackgroundJobClient backgroundJobClient,
            IBus bus
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            _backgroundJobClient = backgroundJobClient ?? throw new ArgumentNullException(nameof(backgroundJobClient));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task Consume(ConsumeContext<DeleteMessageByDelayContract> context)
        {
            _logger.LogInformation($"I will delete the {context.Message.MessageId} message after {context.Message.Delay}");

            var message = await _messageRepository.GetByIdAsync(context.Message.MessageId);

            var whenDeleted = _backgroundJobClient.Schedule(() =>
                _messageRepository.DeleteMessageByIdAsync(context.Message.MessageId), context.Message.Delay);
            //_backgroundJobClient.ContinueJobWith(whenDeleted, () => _bus.Publish());
        }
    }
}
