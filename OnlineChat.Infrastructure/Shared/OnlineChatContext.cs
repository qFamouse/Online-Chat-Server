using Application.Entities;
using Application.Interfaces.Entities;
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

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IEntity &&
                (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                ((IEntity)entry.Entity).UpdatedAt = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    ((IEntity)entry.Entity).CreatedAt = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
