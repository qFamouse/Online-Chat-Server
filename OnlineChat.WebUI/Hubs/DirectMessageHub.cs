using Application.CQRS.Commands.DirectMessage;
using Contracts.Requests.DirectMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OnlineChat.WebUI.Services;
using Services.Interfaces;
using System.Security.Claims;
using Application.Interfaces.Mappers;
using Contracts.Views;

namespace OnlineChat.WebUI.Hubs
{
    [Authorize]
    public class DirectMessageHub : Hub
    {
        private readonly ISender _sender;
        private readonly HubConnectionService _hubConnectionService;
        private readonly IIdentityService _identityService;
        private readonly IDirectMessageMapper _directMessageMapper;
        private Dictionary<int, List<string>> ConnectedUsers => _hubConnectionService.ConnectedUsers;

        public DirectMessageHub
        (
            ISender sender, 
            HubConnectionService hubConnectionService,
            IIdentityService identityService, 
            IDirectMessageMapper directMessageMapper
        )
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
            _hubConnectionService = hubConnectionService ?? throw new ArgumentNullException(nameof(hubConnectionService));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _directMessageMapper = directMessageMapper  ?? throw new ArgumentNullException(nameof(directMessageMapper));
        }

        public async Task<ChatMessageView> SendMessage(SendDirectMessageByReceiverIdRequest request)
        {
            var directMessage = await _sender.Send(new SendDirectMessageByReceiverIdCommand(request));
            var directMessageView = _directMessageMapper.MapToChatMessageView(directMessage);

            if (ConnectedUsers.TryGetValue(request.ReceiverId, out var connections) && connections.Count > 0)
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

                if (ConnectedUsers.TryGetValue(int.Parse(userId), out var connections))
                {
                    connections.Add(connectionID);
                }
                else
                {
                    connections = new List<string>() { connectionID };
                    ConnectedUsers.Add(int.Parse(userId), connections);
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

                if (ConnectedUsers.TryGetValue(int.Parse(userId), out var connections))
                {
                    connections.Remove(connectionID);

                    if (connections.Count == 0)
                    {
                        ConnectedUsers.Remove(int.Parse(userId));
                    }
                }
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
