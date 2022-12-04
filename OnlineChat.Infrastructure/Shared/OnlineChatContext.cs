using Application.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Shared
{
    public class OnlineChatContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<DirectMessage> DirectMessages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationMessage> ConversationMessages { get; set; }
        public DbSet<Participant> Participants { get; set; }

        public OnlineChatContext(DbContextOptions<OnlineChatContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ConversationMessage>()
                .HasOne(cm => cm.Conversation)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Participant>()
                .HasOne(p => p.Conversation)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder);
        }
    }
}
