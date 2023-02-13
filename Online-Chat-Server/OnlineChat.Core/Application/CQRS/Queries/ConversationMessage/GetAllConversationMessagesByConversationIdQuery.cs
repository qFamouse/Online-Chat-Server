using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.ConversationMessage
{
    public class GetAllConversationMessagesByConversationIdQuery : IRequest<IEnumerable<Entities.ConversationMessage>>
    {
        public int ConversationId { get; set; }

        public GetAllConversationMessagesByConversationIdQuery(int conversationId)
        {
            ConversationId = conversationId;
        }
    }
}
