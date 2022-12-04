using System.Security.Claims;

namespace Services.Interfaces
{
    public interface IIdentityService
    {
        ClaimsPrincipal ClaimsPrincipal { get; }
        int Id { get; }
        string Name { get; }
        string Phone { get; }
        string Birthday { get; }
        string Email { get; }
        List<string> Roles { get; }

        // UserView User { get; }

        bool IsAuthenticated { get; }
        bool IsInRole(string role);
    }
}
