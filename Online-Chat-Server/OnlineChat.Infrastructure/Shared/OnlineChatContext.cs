using Application.Entities;
using Application.Interfaces.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Resources;

namespace Shared
{
    public class OnlineChatContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<DirectMessage> DirectMessages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationMessage> ConversationMessages { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        public OnlineChatContext(DbContextOptions<OnlineChatContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole<int>>()
                .HasData(
                    new IdentityRole<int>
                    {
                        Id = 1,
                        Name = Resources.Roles.User,
                        NormalizedName = Resources.Roles.UserNormalized,
                        ConcurrencyStamp = "d9494898-f3f8-4ea9-93d6-2e1a3a5059ac"
                    },
                    new IdentityRole<int>
                    {
                        Id = 2,
                        Name = Resources.Roles.Admin,
                        NormalizedName = Resources.Roles.AdminNormalized,
                        ConcurrencyStamp = "7d04420b-1e0c-4592-9d98-8321feb2a46f"
                    }
                );

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
