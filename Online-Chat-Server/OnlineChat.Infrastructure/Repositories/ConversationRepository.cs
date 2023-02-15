using Application.Interfaces.Repositories;
using Data.Entities;
using Shared;

namespace Repositories
{
    public class ConversationRepository : BaseRepository<Conversation>, IConversationRepository
    {
        public ConversationRepository(OnlineChatContext context) : base(context) { }
    }
}
