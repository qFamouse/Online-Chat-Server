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
        private readonly IBus _bus;

        public AutoMessageDeleteConsumer(ILogger<AutoMessageDeleteConsumer> logger, IMessageRepository messageRepository, IBackgroundJobClient backgroundJobClient, IBus bus)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            _backgroundJobClient = backgroundJobClient ?? throw new ArgumentNullException(nameof(backgroundJobClient));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        public async Task Consume(ConsumeContext<MessageAutoDelete> context)
        {
            _logger.LogInformation($"I will delete the {context.Message.MessageId} message after {context.Message.Delay}");

            var message = await _messageRepository.GetByIdAsync(context.Message.MessageId);

            var whenDeleted = _backgroundJobClient.Schedule(() => 
                _messageRepository.DeleteMessageByIdAsync(context.Message.MessageId), context.Message.Delay);
            _backgroundJobClient.ContinueJobWith(whenDeleted, () => _bus.Publish());
        }
    }
}
