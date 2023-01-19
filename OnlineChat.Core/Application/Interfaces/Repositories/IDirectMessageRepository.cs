using Application.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IDirectMessageRepository : IBaseRepository<DirectMessage>
    {
        Task<IEnumerable<DirectMessage>> GetDirectMessagesByUsersId(int senderId, int receiverId);
        Task<IEnumerable<User>> GetInterlocutorsByUserIdAsync(int userId);
    }
}
