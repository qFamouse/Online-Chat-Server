using Application.Interfaces.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class User : IdentityUser<int>, IEntity
    {
        public string Name { get; set; }
    }
}
