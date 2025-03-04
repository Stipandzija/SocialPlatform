using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ShakSphere.API.Aggregates.PostAggregate.Definitions;
using ShakSphere.Domain.Aggregates.FriendshipAggregate;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;

namespace ShakSphere.Application.DataInterface
{
    public interface IAppDbContext
    {
        DbSet<ApplicationUser> ApplicationUsers { get; }
        DbSet<Post> Posts { get; }
        DbSet<FriendRequest> FriendRequests { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DatabaseFacade Database { get; }
    }
}
