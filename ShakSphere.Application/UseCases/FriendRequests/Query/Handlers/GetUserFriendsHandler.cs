using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShakSphere.Application.Contracts.FriendRequests;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using ShakSphere.Application.UseCases.FriendRequests.Query;


namespace ShakSphere.Application.Friends.Handlers
{
    public class GetUserFriendsQueryHandler : IRequestHandler<GetUserFriendsQuery, ResponseStatus<List<FriendDto>>>
    {
        private readonly IAppDbContext _context;

        public GetUserFriendsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseStatus<List<FriendDto>>> Handle(GetUserFriendsQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus<List<FriendDto>>();

            var user = await _context.ApplicationUsers.Include(u => u.Friends).FirstOrDefaultAsync(u => u.IdentityId == request.UserId.ToString(), cancellationToken);

            if (user == null)
            {
                response.Success = false;
                response.Errors.Add(new ProblemDetails
                {
                    Title = "User not found",
                    Status = 404
                });
                return response;
            }

            var friends = user.Friends.Select(x => new FriendDto
            {
                FriendId = x.AppUserId,
            }).ToList();

            response.Success = true;
            response.Payload = friends;

            return response;
        }
    }
}