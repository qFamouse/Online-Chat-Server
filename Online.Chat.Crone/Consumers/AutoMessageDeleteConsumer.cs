using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Crone;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Online.Chat.Crone.Consumers
{
    public class AutoMessageDeleteConsumer : IConsumer<AutoMessageDelete>
    {
        private readonly ILogger<AutoMessageDeleteConsumer> _logger;

        public AutoMessageDeleteConsumer(ILogger<AutoMessageDeleteConsumer> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Consume(ConsumeContext<AutoMessageDelete> context)
        {
            _logger.LogInformation($"I will delete the {context.Message.MessageId} message after {context.Message.Delay}");
            return Task.CompletedTask;
        }
    }
}
