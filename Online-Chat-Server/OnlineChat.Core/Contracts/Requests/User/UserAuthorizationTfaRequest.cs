namespace Contracts.Requests.User
{
    public class UserAuthorizationTfaRequest
    {
        public string Email { get; set; }
        public int Code {get; set; }
    }
}
