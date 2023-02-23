using Domain.Entities;

namespace Repositories.Abstractions
{
    public interface IMessageRepository
    {
        Task<Message> GetByIdAsync(int id);
        Task DeleteMessageByIdAsync(int id);
    }
}
