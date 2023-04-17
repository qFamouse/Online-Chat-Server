using Configurations;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Services.Abstractions;

namespace Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailConfiguration _configuration;

        public EmailSenderService
        (
            IOptions<EmailConfiguration> options
        )
        {
            _configuration = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task SendEmailAsync(string to, string subject, string content, CancellationToken cancellationToken = default)
        {
            var message = ConvertToMimeMessage(to, subject, content);
            
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_configuration.SmtpServer, _configuration.Port, true, cancellationToken);
                await client.AuthenticateAsync(_configuration.UserName, _configuration.Password, cancellationToken);

                await client.SendAsync(message, cancellationToken);
            }
        }

        private MimeMessage ConvertToMimeMessage(string to, string subject, string content)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("email", _configuration.From));
            message.To.Add(new MailboxAddress("email", to));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = content
            };

            return message;
        }
    }
}
