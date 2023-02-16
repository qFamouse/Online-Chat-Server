using Contracts.Views;
using Contracts.Views.DirectMessage;
using Domain.Entities;
using Mapster;

namespace Application.Interfaces.Mappers;

[Mapper]
public interface IDirectMessageMapper
{
    DirectMessageView MapToView(DirectMessage message);
    IEnumerable<DirectMessageView> MapToView(IEnumerable<DirectMessage> message);

    ChatMessageView MapToChatMessageView(DirectMessage message);

    ChatMessageDetailView MapToDetailView(DirectMessage message);
    IEnumerable<ChatMessageDetailView> MapToDetailView(IEnumerable<DirectMessage> message);
}