using Application.Interfaces.Repositories;
using Data.Entities;
using Shared;

namespace Repositories
{
    public class ConversationMessagesRepository : BaseRepository<ConversationMessage>, IConversationMessagesRepository
    {
        public ConversationMessagesRepository(OnlineChatContext context) : base(context) { }
    }
}
