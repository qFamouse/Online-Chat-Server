using Application.CQRS.Queries.Conversation;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.QueryHandlers.Conversation
{
    internal class GetConversationByIdQueryHandler : IRequestHandler<GetConversationByIdQuery, Entities.Conversation>
    {
        private readonly IConversationRepository _conversationRepository;

        public GetConversationByIdQueryHandler
        (
            IConversationRepository conversationRepository
        )
        {
            _conversationRepository = conversationRepository ?? throw new ArgumentNullException(nameof(conversationRepository));
        }

        public async Task<Entities.Conversation> Handle(GetConversationByIdQuery request, CancellationToken cancellationToken)
        {
            return await _conversationRepository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
