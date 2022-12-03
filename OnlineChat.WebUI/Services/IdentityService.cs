using Services.Interfaces;
using System.Security.Claims;

namespace OnlineChat.WebUI.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpContext _httpContext;
        private readonly ClaimsIdentity _claimsIdentity;

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext ??
                           throw new ArgumentNullException(nameof(httpContextAccessor), "Interface is null");
            _claimsIdentity = _httpContext.User.Identity as ClaimsIdentity;
        }

        #region Claims

        public ClaimsPrincipal ClaimsPrincipal => _httpContext.User;
        public int Id => GetId();
        public string Name => _claimsIdentity.Name;
        public string Phone => GetClaim(ClaimTypes.MobilePhone)?.Value;
        public string Birthday => GetClaim(ClaimTypes.DateOfBirth)?.Value;
        public string Email => GetClaim(ClaimTypes.Email)?.Value;
        public List<string> Roles => _claimsIdentity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

        // public UserView User => new UserView(Id, Name, Phone, Birthday, Email, Roles);

        #endregion

        #region States

        public bool IsAuthenticated => _claimsIdentity.IsAuthenticated;
        public bool IsInRole(string role) => _httpContext.User.IsInRole(role);

        #endregion

        private int GetId()
        {
            string id = GetClaim(ClaimTypes.NameIdentifier)?.Value;
            return String.IsNullOrEmpty(id) ? 0 : Int32.Parse(id);
        }

        private Claim GetClaim(string claimType) => _claimsIdentity.Claims.SingleOrDefault(c => c.Type == claimType);
    }
}
