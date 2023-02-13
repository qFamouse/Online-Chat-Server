namespace Contracts.Requests.User
{
    public class UserAuthorizationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
