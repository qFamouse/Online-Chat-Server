using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using Contracts.Views;
using Contracts.Views.DirectMessage;
using Mapster;

namespace Application.Interfaces.Mappers
{
    [Mapper]
    public interface IDirectMessageMapper
    {
        DirectMessageView MapToView(DirectMessage message);
        IEnumerable<DirectMessageView> MapToView(IEnumerable<DirectMessage> message);

        ChatMessageDetailView MapToDetailView(DirectMessage message);
        IEnumerable<ChatMessageDetailView> MapToDetailView(IEnumerable<DirectMessage> message);
    }
}
