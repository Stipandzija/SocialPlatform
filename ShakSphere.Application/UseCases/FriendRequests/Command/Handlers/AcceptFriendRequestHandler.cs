using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using ShakSphere.Application.UseCases.FriendRequests.Command;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;


namespace ShakSphere.Application.Friends.Handlers
{
    public class AcceptFriendRequestHandler : IRequestHandler<AcceptFriendRequestCommand, ResponseStatus<bool>>
    {
        private readonly IAppDbContext _context;

        public AcceptFriendRequestHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseStatus<bool>> Handle(AcceptFriendRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus<bool>();
            var user = await _context.ApplicationUsers.Include(u => u.Friends)
                                                      .FirstOrDefaultAsync(u => u.IdentityId == request.UserId.ToString(), cancellationToken);

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

            var friendRequest = await _context.FriendRequests.Where(fr => fr.RequestId == request.RequestId).FirstOrDefaultAsync(cancellationToken);

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

            if (user.AppUserId != friendRequest.ReceiverId)
            {
                response.Success = false;
                response.Errors.Add(new ProblemDetails
                {
                    Title = "Unauthorized action",
                    Status = 403
                });

                return response;
            }

            var receiver = await _context.ApplicationUsers.Include(u => u.Friends).FirstOrDefaultAsync(u => u.AppUserId == friendRequest.SenderId, cancellationToken);

            if (user == null || receiver == null)
            {
                response.Success = false;
                response.Errors.Add(new ProblemDetails
                {
                    Title = "One or both users not found",
                    Status = 404
                });

                return response;
            }
            try
            {
                user.AddFriend(receiver);
                receiver.AddFriend(user);

                _context.FriendRequests.Remove(friendRequest);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
               
                response.Success = false;
                response.Errors.Add(new ProblemDetails
                {
                    Title = "Concurrency issue",
                    Detail = "Friend request was modified or deleted before your action",
                    Status = 409
                });
                return response;
                
            }
            response.Success = true;
            return response;
        }

    }
}
