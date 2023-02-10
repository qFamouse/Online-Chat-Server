using OnlineChat.Cron.Entities;

namespace OnlineChat.Cron.Repositories.Abstractions
{
    public interface IMessageRepository
    {
        Task<Message> GetByIdAsync(int id);
        Task DeleteMessageByIdAsync(int id);
    }
}
