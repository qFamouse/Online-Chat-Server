namespace Contracts.Contracts
{
    public record MessageDeleteContract
    {
        public int MessageId { get; set; }
        public TimeSpan Delay { get; set; }
    }
}
