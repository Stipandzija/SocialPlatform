using MediatR;
using Microsoft.EntityFrameworkCore;
using ShakSphere.Application.Contracts.FriendRequests;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using ShakSphere.Domain.Aggregates.FriendshipAggregate;

namespace ShakSphere.Application.UseCases.FriendRequests.Query.Handlers
{
    public class GetFollowRequestsQueryHandler : IRequestHandler<GetFollowRequestsQuery, ResponseStatus<List<FriendRequestDto>>>
    {
        private readonly IAppDbContext _context;

        public GetFollowRequestsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseStatus<List<FriendRequestDto>>> Handle(GetFollowRequestsQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus<List<FriendRequestDto>>();

            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.IdentityId == request.UserId.ToString(), cancellationToken);

            if (user == null)
            {
                response.Success = false;
                response.Errors.Add(new Microsoft.AspNetCore.Mvc.ProblemDetails
                {
                    Title = "User not found",
                    Status = 404
                });
                return response;
            }

            var friendRequests = await _context.FriendRequests
                .Where(fr => fr.ReceiverId == user.AppUserId && fr.Status == FriendRequestStatus.Pending)
                .Select(fr => new FriendRequestDto
                {
                    RequestId = fr.RequestId,
                    SenderId = fr.SenderId,
                })
                .ToListAsync(cancellationToken);

            response.Payload = friendRequests;
            return response;
        }
    }
}