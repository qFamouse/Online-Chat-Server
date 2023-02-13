using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.Conversation
{
    public class GetConversationByIdQuery : IRequest<Entities.Conversation>
    {
        public int Id { get; set; }

        public GetConversationByIdQuery(int id)
        {
            Id = id;
        }
    }
}
