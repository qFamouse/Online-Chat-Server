using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NuGet.MassTransit.Contracts;
using OnlineChat.WebUI.Hubs;
using Services.Abstractions;

namespace OnlineChat.WebUI.Consumers
{
    public class MessageHasBeenDeletedConsumer : IConsumer<MessageHasBeenDeletedContract>
    {
        private readonly IHubContext<DirectMessageHub> _hubContext;
        private readonly IHubConnectionsService _hubConnectionsService;


        public MessageHasBeenDeletedConsumer(IHubContext<DirectMessageHub> hubContext, IHubConnectionsService hubConnectionsService)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _hubConnectionsService = hubConnectionsService ?? throw new ArgumentNullException(nameof(hubConnectionsService));
        }

        public async Task Consume(ConsumeContext<MessageHasBeenDeletedContract> context)
        {
            if (_hubConnectionsService
                    .TryGetValues(out var connections, context.Message.SenderId, context.Message.ReceiverId))
            {
                foreach (var client in connections.Select(connectionId => _hubContext.Clients.Client(connectionId)))
                {
                    await client.SendAsync("DeleteMessage", context.Message);
                }
            }
        }
    }
}
