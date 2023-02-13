namespace OnlineChat.MassTransit.Contracts;

public record DeleteMessageByDelayContract
{
    public int MessageId { get; set; }
    public TimeSpan Delay { get; set; }
}