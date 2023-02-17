using Contracts.Requests.DirectMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
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
            var userIdentity = Context.GetHttpContext()?.User.Identity as ClaimsIdentity;
            if (userIdentity != null)
            {
                string connectionID = Context.ConnectionId;
                string userId = userIdentity.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

                if (_hubConnectionsService.TryGetValue(int.Parse(userId), out var connections))
                {
                    connections.Add(connectionID);
                }
                else
                {
                    connections = new List<string>() { connectionID };
                    _hubConnectionsService.Add(int.Parse(userId), connections);
                }
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userIdentity = Context.GetHttpContext()?.User.Identity as ClaimsIdentity;
            if (userIdentity != null)
            {
                string connectionID = Context.ConnectionId;
                string userId = userIdentity.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

                if (_hubConnectionsService.TryGetValue(int.Parse(userId), out var connections))
                {
                    connections.Remove(connectionID);

                    if (connections.Count == 0)
                    {
                        _hubConnectionsService.Remove(int.Parse(userId));
                    }
                }
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
