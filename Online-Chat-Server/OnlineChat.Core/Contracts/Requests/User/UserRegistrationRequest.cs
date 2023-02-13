namespace Contracts.Requests.User
{
    public class UserSignupRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
