namespace Services.Abstractions
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string to, string subject, string content, CancellationToken cancellationToken = default);
    }
}
