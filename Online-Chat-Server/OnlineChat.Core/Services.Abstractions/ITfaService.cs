namespace Services.Abstractions
{
    public interface ITfaService
    {
        Task<bool> AuthenticateAsync(string email, string code);
        Task StartAuthenticationAsync(string email);
    }
}
