using Hellang.Middleware.ProblemDetails;
using Resources.Messages;
using Services.Abstractions;
using StackExchange.Redis;

namespace Services
{
    public class TfaService : ITfaService
    {
        private readonly IDatabase _redis;

        public TfaService
        (
            IConnectionMultiplexer multiplexer)
        {
            _redis = multiplexer.GetDatabase();
        }

        public async Task<bool> AuthenticateAsync(string email, int code)
        {
            RedisValue value = await _redis.StringGetAsync(email);


            if (value.IsNull || value != code)
            {
                throw new ProblemDetailsException(400, TfaMessages.InvalidCode);
            }

            
            return true;
        }

        public async Task SendNewAuthenticationCodeAsync(string email)
        {
            int code = new Random().Next(100000, 999999);

            await _redis.StringSetAsync(email, code, TimeSpan.FromMinutes(15));
        }
    }
}
