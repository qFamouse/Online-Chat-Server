using Application.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class OnlineChatContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<DirectMessage> DirectMessages { get; set; }

        public OnlineChatContext(DbContextOptions<OnlineChatContext> options) : base(options) { }
    }
}
