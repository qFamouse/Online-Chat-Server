using Application.Interfaces.Repositories;
using Shared;
using Data.Entities;

namespace Repositories
{
    public class ConversationRepository : BaseRepository<Conversation>, IConversationRepository
    {
        public ConversationRepository(OnlineChatContext context) : base(context) { }
    }
}
