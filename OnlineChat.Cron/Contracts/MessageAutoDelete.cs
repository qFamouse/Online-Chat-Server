namespace OnlineChat.Cron.Contracts
{
    public record MessageAutoDelete
    {
        public int MessageId { get; set; }
        public TimeSpan Delay { get; set; }
    }
}
