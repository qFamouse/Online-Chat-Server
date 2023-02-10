using Hangfire;
using MassTransit;
using OnlineChat.Cron.Contracts;
using OnlineChat.Cron.Repositories.Abstractions;

namespace OnlineChat.Cron.Consumers
{
    public class AutoMessageDeleteConsumer : IConsumer<MessageAutoDelete>
    {
        private readonly ILogger<AutoMessageDeleteConsumer> _logger;
        private readonly IMessageRepository _messageRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public AutoMessageDeleteConsumer(ILogger<AutoMessageDeleteConsumer> logger, IMessageRepository messageRepository, IBackgroundJobClient backgroundJobClient)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            _backgroundJobClient = backgroundJobClient ?? throw new ArgumentNullException(nameof(backgroundJobClient));
        }

        public Task Consume(ConsumeContext<MessageAutoDelete> context)
        {
            _logger.LogInformation($"I will delete the {context.Message.MessageId} message after {context.Message.Delay}");

            _backgroundJobClient.Schedule(() => 
                _messageRepository.DeleteMessageByIdAsync(context.Message.MessageId), context.Message.Delay);

            return Task.CompletedTask;
        }
    }
}
