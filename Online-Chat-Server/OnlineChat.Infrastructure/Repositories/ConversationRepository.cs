using Domain.Entities;
using Repositories.Abstractions;
using Shared;

namespace Repositories
{
    public class ConversationRepository : BaseRepository<Conversation>, IConversationRepository
    {
        public ConversationRepository(OnlineChatContext context) : base(context) { }
    }
}
