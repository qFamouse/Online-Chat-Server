using Hellang.Middleware.ProblemDetails;
using Resources.Messages;
using Services.Abstractions;
using StackExchange.Redis;

namespace Services
{
    public class TfaService : ITfaService
    {
        private readonly IDatabase _redis;
        private readonly IEmailSenderService _emailSenderService;

        public TfaService
        (
            IConnectionMultiplexer multiplexer,
            IEmailSenderService emailSenderService
        )
        {
            _redis = multiplexer.GetDatabase();
            _emailSenderService = emailSenderService;
        }

        public async Task<bool> AuthenticateAsync(string email, int code)
        {
            RedisValue value = await _redis.StringGetAsync(email);

            return !value.IsNull && value == code;
        }

        public async Task SendNewAuthenticationCodeAsync(string email)
        {
            int code = new Random().Next(100000, 999999);

            await _redis.StringSetAsync(email, code, TimeSpan.FromMinutes(15));
            await _emailSenderService.SendEmailAsync(email, "Authentication Code", code.ToString());
        }
    }
}
