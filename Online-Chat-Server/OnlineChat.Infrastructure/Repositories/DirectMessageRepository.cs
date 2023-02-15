using Application.Interfaces.Repositories;
using Data.Entities;
using Data.Functions;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Repositories
{
    public class DirectMessageRepository : BaseRepository<DirectMessage>, IDirectMessageRepository
    {
        public DirectMessageRepository(OnlineChatContext context) : base(context) { }

        private IQueryable<DirectMessage> GetDirectMessagesByUsersId(int senderId, int receiverId)
        {
            var fromSender = DbContext.DirectMessages.Where(dm => dm.SenderId == senderId && dm.ReceiverId == receiverId);
            var fromReceiver = DbContext.DirectMessages.Where(dm => dm.SenderId == receiverId && dm.ReceiverId == senderId);

            return fromSender.Union(fromReceiver);
        }

        public async Task<IEnumerable<DirectMessage>> GetDirectMessagesByUsersIdAsync(int senderId, int receiverId, CancellationToken cancellationToken = default)
        {
            return await GetDirectMessagesByUsersId(senderId, receiverId).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<DirectMessage>> GetDetailDirectMessagesByUsersIdAsync(int senderId, int receiverId, CancellationToken cancellationToken = default)
        {
            return await GetDirectMessagesByUsersId(senderId, receiverId)
                .Include(dm => dm.Attachments)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<User>> GetInterlocutorsByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            var senders = DbContext.DirectMessages
                .Where(dm => dm.ReceiverId == userId)
                .Select(dm => dm.Sender);

            var receivers = DbContext.DirectMessages
                .Where(dm => dm.SenderId == userId)
                .Select(dm => dm.Receiver);

            return await senders.Union(receivers).Distinct().ToListAsync(cancellationToken);
        }

        public async Task<DirectMessageStatistics> GetDirectMessageStatisticsByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            var statistics = await DbContext
                .Set<DirectMessageStatistics>()
                .FromSqlRaw($"SELECT * FROM GetStatisticByUserId({userId})")
                .FirstAsync(cancellationToken);

            return statistics;
        }
    }
}
