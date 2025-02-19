using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ShakSphere.API.Aggregates.PostAggregate.Definitions;
using ShakSphere.Application.DataInterface;
using ShakSphere.Domain.Aggregates.PostAggregate.Definitions;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;

namespace ShakSphere.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>, IAppDbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        public DbSet<IdentityUser> IdentityAppUsers { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        public DatabaseFacade Database => base.Database;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
                .HasKey(au => au.AppUserId);

            builder.Entity<Post>()
                .HasKey(p => p.PostId);
            builder.Entity<Post>()
                .HasOne(p => p.AppUser)
                .WithMany()
                .HasForeignKey(p => p.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

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

            builder.Entity<PostInteraction>(entity =>
            {
                entity.HasKey(p => p.InteractionId);
            });
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.OwnsOne(p => p.BasicInfo);
            });

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name ="Admin",
                    NormalizedName="ADMIN"
                },
                new IdentityRole
                {
                    Name ="User",
                    NormalizedName="USER"
                }
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
