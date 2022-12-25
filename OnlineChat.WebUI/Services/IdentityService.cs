using Services.Interfaces;
using System.Linq;
using System.Security.Claims;

namespace OnlineChat.WebUI.Services
{
    public class IdentityService : IIdentityService
    {

        private readonly HttpContext _httpContext;
        private readonly ClaimsIdentity _claimsIdentity;

        //public ClaimsPrincipal ClaimsPrincipal => _httpContext.User;

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor.HttpContext));
            _claimsIdentity = _httpContext.User.Identity as ClaimsIdentity ?? throw new ArgumentNullException(nameof(_httpContext));
        }

        private string GetClaim(string type)
        {
            return _claimsIdentity.Claims.SingleOrDefault(c => c.Type == type)?.Value ?? throw new ArgumentNullException(type);
        }

        public int GetUserId()
        {
            return int.Parse(GetClaim(ClaimTypes.NameIdentifier));
        }

        public string GetUserName()
        {
            return GetClaim(ClaimTypes.Name);
        }

        public string GetUserEmail()
        {
            return GetClaim(ClaimTypes.Email);
        }

        public IList<string> GetUserRoles()
        {
            return _claimsIdentity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        }

        public bool UserIsInRole(string role)
        {
            return _httpContext.User.IsInRole(role);
        }

        public bool UserIsAuthenticated => _claimsIdentity.IsAuthenticated;
    }
}
