using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Infrastructure.Data
{
    public class OnlineChatContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public OnlineChatContext(DbContextOptions<OnlineChatContext> options) : base(options) { }
    }
}
