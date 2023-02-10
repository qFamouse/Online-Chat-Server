namespace Contracts.Crone
{
    public record AutoMessageDelete
    {
        public int MessageId { get; set; }
        public TimeSpan Delay { get; set; }
    }
}
