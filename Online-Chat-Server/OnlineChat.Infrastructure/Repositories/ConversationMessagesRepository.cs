using Application.Interfaces.Repositories;
using Shared;
using Data.Entities;

namespace Repositories
{
    public class ConversationMessagesRepository : BaseRepository<ConversationMessage>, IConversationMessagesRepository
    {
        public ConversationMessagesRepository(OnlineChatContext context) : base(context) { }
    }
}
