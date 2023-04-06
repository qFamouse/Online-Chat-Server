namespace Contracts.Views.User
{
    public class UserAuthorizationView
    {
        public bool IsAuthSuccessful { get; set; }
        public bool IsTfaEnabled { get; set; }
        public string? Token { get; set; }
    }
}
