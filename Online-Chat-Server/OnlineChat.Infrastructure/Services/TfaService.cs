using Services.Abstractions;

namespace Services
{
    public class TfaService : ITfaService
    {
        public Task<bool> AuthenticateAsync(string email, string code)
        {
            throw new NotImplementedException();
        }

        public Task StartAuthenticationAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
