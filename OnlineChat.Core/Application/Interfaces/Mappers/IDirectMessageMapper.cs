using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using Contracts.Views;
using Mapster;

namespace Application.Interfaces.Mappers
{
    [Mapper]
    public interface IDirectMessageMapper
    {
        DirectMessageView Map(DirectMessage message);
        IEnumerable<ChatMessageDetailView> Map(IEnumerable<DirectMessage> message);
    }
}
