using Application.Interfaces.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Entities
{
    public class User : IdentityUser<int>, IEntity
    {

    }
}
