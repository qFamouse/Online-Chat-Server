using Application.Entities;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Repositories
{
    public class DirectMessageRepository : BaseRepository<DirectMessage>, IDirectMessageRepository
    {
        public DirectMessageRepository(OnlineChatContext context) : base(context) { }

        public async Task<IEnumerable<DirectMessage>> GetDirectMessagesByUsersIdAsync(int senderId, int receiverId)
        {
            var fromSender = DbContext.DirectMessages.Where(dm => dm.SenderId == senderId && dm.ReceiverId == receiverId);
            var fromReceiver = DbContext.DirectMessages.Where(dm => dm.SenderId == receiverId && dm.ReceiverId == senderId);

            return await fromSender.Union(fromReceiver).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetInterlocutorsByUserIdAsync(int userId)
        {
            var senders = DbContext.DirectMessages
                .Where(dm => dm.ReceiverId == userId)
                .Select(dm => dm.Sender);

            var receivers = DbContext.DirectMessages
                .Where(dm => dm.SenderId == userId)
                .Select(dm => dm.Receiver);

            return await senders.Union(receivers).Distinct().ToListAsync();
        }
    }
}
