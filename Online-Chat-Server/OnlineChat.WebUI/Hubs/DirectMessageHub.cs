using Contracts.Requests.DirectMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Application.CQRS.Commands.DirectMessages;
using Contracts.Views;
using Application.Mappers.Abstractions;
using Services.Abstractions;

namespace OnlineChat.WebUI.Hubs
{
    [Authorize]
    public class DirectMessageHub : Hub
    {
        private readonly ISender _sender;
        private readonly IHubConnectionsService _hubConnectionsService;
        private readonly IIdentityService _identityService;
        private readonly IDirectMessageMapper _directMessageMapper;

        public DirectMessageHub
        (
            ISender sender,
            IHubConnectionsService hubConnectionsService,
            IIdentityService identityService, 
            IDirectMessageMapper directMessageMapper
        )
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
            _hubConnectionsService = hubConnectionsService ?? throw new ArgumentNullException(nameof(hubConnectionsService));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _directMessageMapper = directMessageMapper  ?? throw new ArgumentNullException(nameof(directMessageMapper));
        }

        public async Task<ChatMessageView> SendMessage(SendDirectMessageByReceiverIdRequest request)
        {
            var directMessage = await _sender.Send(new SendDirectMessageByReceiverIdCommand(request));
            var directMessageView = _directMessageMapper.MapToChatMessageView(directMessage);

            if (_hubConnectionsService.TryGetValue(request.ReceiverId, out var connections) && connections.Count > 0)
            {
                foreach (var client in connections.Select(connectionId => Clients.Client(connectionId)))
                {
                    await client.SendAsync("ReceiveMessage", directMessage);
                }
            }

            return directMessageView;
        }

        public override async Task OnConnectedAsync()
        {
            if (!_identityService.UserIsAuthenticated)
            {
                await base.OnConnectedAsync();
            }

            var connectionId = Context.ConnectionId;
            var userId = _identityService.GetUserId();

            if (_hubConnectionsService.TryGetValue(userId, out var connections))
            {
                connections.Add(connectionId);
            }
            else
            {
                connections = new List<string> { connectionId };
                _hubConnectionsService.Add(userId, connections);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (!_identityService.UserIsAuthenticated)
            {
                await base.OnDisconnectedAsync(exception);
            }

            var connectionId = Context.ConnectionId;
            var userId = _identityService.GetUserId();

            if (_hubConnectionsService.TryGetValue(userId, out var connections))
            {
                connections.Remove(connectionId);

                if (connections.Count == 0)
                {
                    _hubConnectionsService.Remove(userId);
                }
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
