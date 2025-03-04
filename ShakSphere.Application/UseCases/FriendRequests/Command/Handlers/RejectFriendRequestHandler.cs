using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using ShakSphere.Application.UseCases.FriendRequests.Command;

namespace ShakSphere.Application.Friends.Handlers
{
    public class RejectFriendRequestHandler : IRequestHandler<RejectFriendRequestCommand, ResponseStatus<bool>>
    {
        private readonly IAppDbContext _context;

        public RejectFriendRequestHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseStatus<bool>> Handle(RejectFriendRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus<bool>();
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
            var friendRequest = _context.FriendRequests.FirstOrDefault(fr => fr.RequestId == request.RequestId);

            if (friendRequest == null)
            {
                response.Success = false;
                response.Errors.Add(new ProblemDetails
                {
                    Title = "Friend request not found",
                    Status = 404
                });

                return response;
            }

            _context.FriendRequests.Remove(friendRequest);
            await _context.SaveChangesAsync(cancellationToken);

            response.Payload = true;
            return response;
        }
    }
}
