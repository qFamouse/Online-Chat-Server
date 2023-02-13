using Application.CQRS.Queries.ConversationMessage;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.QueryHandlers.ConversationMessage
{
    internal class GetAllConversationMessagesByConversationIdQueryHandler : IRequestHandler<GetAllConversationMessagesByConversationIdQuery, IEnumerable<Entities.ConversationMessage>>
    {
        private readonly IConversationMessagesRepository _conversationMessagesRepository;

        public GetAllConversationMessagesByConversationIdQueryHandler
        (
            IConversationMessagesRepository conversationMessagesRepository
        )
        {
            _conversationMessagesRepository = conversationMessagesRepository;
        }

        public Task<IEnumerable<Entities.ConversationMessage>> Handle(GetAllConversationMessagesByConversationIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
