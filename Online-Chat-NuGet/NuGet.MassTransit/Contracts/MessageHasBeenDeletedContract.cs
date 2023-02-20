namespace NuGet.MassTransit.Contracts;

public record MessageHasBeenDeletedContract
{
    public int MessageId { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
}