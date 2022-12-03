namespace Contracts.Views
{
    public class UserAuthorizationView
    {
        public string Token { get; }
        public DateTime Expiration { get; }

        public UserAuthorizationView(string token, DateTime expiration)
        {
            Token = token;
            Expiration = expiration;
        }
    }
}
