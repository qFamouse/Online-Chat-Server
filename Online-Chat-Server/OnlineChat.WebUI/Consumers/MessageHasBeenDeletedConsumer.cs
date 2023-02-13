using MassTransit;
using Microsoft.AspNetCore.SignalR;
using OnlineChat.MassTransit.Contracts;
using OnlineChat.WebUI.Hubs;
using OnlineChat.WebUI.Services;

namespace OnlineChat.WebUI.Consumers
{
    public class MessageHasBeenDeletedConsumer : IConsumer<MessageHasBeenDeletedContract>
    {
        private readonly IHubContext<DirectMessageHub> _hubContext;
        private readonly HubConnectionService _hubConnectionService;

        private Dictionary<int, List<string>> ConnectedUsers => _hubConnectionService.ConnectedUsers;

        public MessageHasBeenDeletedConsumer(IHubContext<DirectMessageHub> hubContext, HubConnectionService connectionService)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _hubConnectionService = connectionService ?? throw new ArgumentNullException(nameof(connectionService));
        }

        public async Task Consume(ConsumeContext<MessageHasBeenDeletedContract> context)
        {
            var connections = ConnectedUsers
                .Where(x => x.Key == context.Message.SenderId || x.Key == context.Message.ReceiverId)
                .Select(x => x.Value)
                .SelectMany(list => list)
                .ToList();

            if (connections.Count > 0)
            {
                foreach (var client in connections.Select(connectionId => _hubContext.Clients.Client(connectionId)))
                {
                    await client.SendAsync("DeleteMessage", context.Message);
                }
            }
        }
    }
}
