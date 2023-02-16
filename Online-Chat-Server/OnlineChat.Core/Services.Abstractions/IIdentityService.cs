namespace Services.Abstractions
{
    public interface IIdentityService
    {
        int GetUserId();
        string GetUserName();
        string GetUserEmail();
        IList<string> GetUserRoles();
        bool UserIsInRole(string role);
        bool UserIsAuthenticated { get; }
    }
}
