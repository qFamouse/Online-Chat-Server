using Application.Interfaces.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Entities
{
    public class User : IdentityUser<int>, IEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
