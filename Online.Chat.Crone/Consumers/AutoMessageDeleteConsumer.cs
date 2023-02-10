using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Crone;
using MassTransit;
using Microsoft.Extensions.Logging;
using Online.Chat.Crone.Repositories.Abstractions;

namespace Online.Chat.Crone.Consumers
{
    public class AutoMessageDeleteConsumer : IConsumer<AutoMessageDelete>
    {
        private readonly ILogger<AutoMessageDeleteConsumer> _logger;
        private readonly IMessageRepository _messageRepository;

        public AutoMessageDeleteConsumer(ILogger<AutoMessageDeleteConsumer> logger, IMessageRepository messageRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
        }

        public Task Consume(ConsumeContext<AutoMessageDelete> context)
        {
            _logger.LogInformation($"I will delete the {context.Message.MessageId} message after {context.Message.Delay}");

            _messageRepository.DeleteMessageById(context.Message.MessageId);

            return Task.CompletedTask;
        }
    }
}
