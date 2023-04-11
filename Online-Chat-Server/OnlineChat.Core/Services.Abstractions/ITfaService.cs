namespace Services.Abstractions
{
    public interface ITfaService
    {
        Task<bool> AuthenticateAsync(string email, int code);
        Task SendNewAuthenticationCodeAsync(string email);
    }
}
