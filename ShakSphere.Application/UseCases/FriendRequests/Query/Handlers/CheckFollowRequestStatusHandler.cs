using MediatR;
using Microsoft.EntityFrameworkCore;
using ShakSphere.Application.Contracts.FriendRequests;
using ShakSphere.Application.DataInterface;

namespace ShakSphere.Application.UseCases.FriendRequests.Query.Handlers
{
    public class CheckFollowRequestStatusHandler : IRequestHandler<CheckFollowRequestStatusQuery, FollowRequestStatusDto>
    {
        private readonly IAppDbContext _context;

        public CheckFollowRequestStatusHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<FollowRequestStatusDto> Handle(CheckFollowRequestStatusQuery request, CancellationToken cancellationToken)
        {
            var requestExists = await _context.FriendRequests.AnyAsync(fr =>
                    fr.SenderId == request.SenderId && fr.ReceiverId == request.ReceiverId ||
                    fr.SenderId == request.ReceiverId && fr.ReceiverId == request.SenderId,
                    cancellationToken);

            return new FollowRequestStatusDto
            {
                Exists = requestExists
            };
        }
    }
}
