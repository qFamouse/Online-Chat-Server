using Data.Entities;
using Repositories.Abstractions;
using Shared;

namespace Repositories
{
    public class ConversationMessagesRepository : BaseRepository<ConversationMessage>, IConversationMessagesRepository
    {
        public ConversationMessagesRepository(OnlineChatContext context) : base(context) { }
    }
}
