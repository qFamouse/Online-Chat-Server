using Data.Entities;
using Data.Functions;

namespace Application.Interfaces.Repositories;

public interface IDirectMessageRepository : IBaseRepository<DirectMessage>
{
    Task<IEnumerable<DirectMessage>> GetDirectMessagesByUsersIdAsync(int senderId, int receiverId, CancellationToken cancellationToken = default);
    Task<IEnumerable<DirectMessage>> GetDetailDirectMessagesByUsersIdAsync(int senderId, int receiverId, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetInterlocutorsByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<DirectMessageStatistics> GetDirectMessageStatisticsByUserIdAsync(int userId, CancellationToken cancellationToken = default);
}