using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ShakSphere.API.Aggregates.PostAggregate.Definitions;
using ShakSphere.Application.DataInterface;
using ShakSphere.Domain.Aggregates.FriendshipAggregate;
using ShakSphere.Domain.Aggregates.PostAggregate.Definitions;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ShakSphere.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<IdentityUser> IdentityAppUsers { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostInteraction> PostInteractions { get; set; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public DatabaseFacade Database => base.Database;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ApplicationUser
            builder.Entity<ApplicationUser>()
                .HasKey(u => u.AppUserId);

            builder.Entity<ApplicationUser>()
                .OwnsOne(u => u.BasicInfo);

            builder.Entity<ApplicationUser>()
                .HasIndex(u => u.IdentityId)
                .IsUnique();
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Friends)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "UserFriends",
                    j => j.HasOne<ApplicationUser>().WithMany().HasForeignKey("FriendId"),
                    j => j.HasOne<ApplicationUser>().WithMany().HasForeignKey("UserId"),
                    j => j.HasKey("UserId", "FriendId"));
            builder.Entity<FriendRequest>()
                .HasKey(fr => fr.RequestId);

            builder.Entity<FriendRequest>()
                .HasOne(fr => fr.Sender)
                .WithMany()
                .HasForeignKey(fr => fr.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<FriendRequest>()
                .HasOne(fr => fr.Receiver)
                .WithMany()
                .HasForeignKey(fr => fr.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Post
            builder.Entity<Post>()
                .HasKey(p => p.PostId);

            builder.Entity<Post>()
                .HasOne(p => p.AppUser)
                .WithMany()
                .HasForeignKey(p => p.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            // PostComment
            builder.Entity<PostComment>()
                .HasKey(pc => pc.CommentId);

            builder.Entity<PostComment>()
                .HasOne<Post>()
                .WithMany(p => p.Comments)
                .HasForeignKey(pc => pc.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PostComment>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(pc => pc.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // PostInteraction
            builder.Entity<PostInteraction>()
                .HasKey(pi => pi.InteractionId);

            // Roles
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "User", NormalizedName = "USER" }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}