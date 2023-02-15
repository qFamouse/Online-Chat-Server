﻿using Contracts.Views;
using MediatR;

namespace Application.CQRS.Queries.DirectMessage;

public class GetDirectChatByReceiverIdQuery : IRequest<IEnumerable<ChatMessageDetailView>>
{
    public int ReceiverId { get; set; }

    public GetDirectChatByReceiverIdQuery(int receiverId)
    {
        ReceiverId = receiverId;
    }
}