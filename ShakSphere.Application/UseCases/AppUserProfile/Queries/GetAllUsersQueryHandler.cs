using MediatR;
using Microsoft.EntityFrameworkCore;
using ShakSphere.Application.DataInterface;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;


namespace ShakSphere.Application.UseCases.AppUserProfile.Queries
{
    internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<ApplicationUser>>
    {
        private readonly IAppDbContext _context;
        public GetAllUsersQueryHandler(IAppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<List<ApplicationUser>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken) 
        {
            return await _context.ApplicationUsers.Include(x=>x.BasicInfo).ToListAsync(cancellationToken);
        }
    }
}
