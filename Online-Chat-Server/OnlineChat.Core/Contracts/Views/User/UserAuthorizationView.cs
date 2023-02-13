namespace Contracts.Views.User
{
    public class UserAuthorizationView
    {
        public string Token { get; }
        public DateTime Expires { get; }

        public UserAuthorizationView(string token, DateTime expires)
        {
            Token = token;
            Expires = expires;
        }
    }
}
