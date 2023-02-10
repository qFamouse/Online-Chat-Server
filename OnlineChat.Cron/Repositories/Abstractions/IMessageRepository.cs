namespace OnlineChat.Cron.Repositories.Abstractions
{
    public interface IMessageRepository
    {
        Task DeleteMessageByIdAsync(int id);
    }
}
