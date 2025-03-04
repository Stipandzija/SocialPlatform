using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using ShakSphere.Application.UseCases.FriendRequests.Command;
using ShakSphere.Domain.Aggregates.FriendshipAggregate;

namespace ShakSphere.Application.Friends.Handlers
{
    public class SendFriendRequestHandler : IRequestHandler<SendFriendRequestCommand, ResponseStatus<bool>>
    {
        private readonly IAppDbContext _context;

        public SendFriendRequestHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseStatus<bool>> Handle(SendFriendRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus<bool>();

            var user = await _context.ApplicationUsers.Include(u => u.Friends).FirstOrDefaultAsync(u => u.IdentityId == request.SenderId.ToString(), cancellationToken);

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

            var receiver = await _context.ApplicationUsers.Include(u => u.Friends).FirstOrDefaultAsync(u => u.AppUserId == request.ReceiverId, cancellationToken);

            if (receiver == null)
            {
                response.Success = false;
                response.Errors.Add(new ProblemDetails
                {
                    Title = "Receiver not found",
                    Status = 404
                });
                return response;
            }

            var existingRequest = _context.FriendRequests.FirstOrDefault(fr =>
                (fr.SenderId == request.SenderId && fr.ReceiverId == request.ReceiverId) ||
                (fr.SenderId == request.ReceiverId && fr.ReceiverId == request.SenderId));

            if (existingRequest != null)
            {
                response.Success = false;
                response.Errors.Add(new ProblemDetails
                {
                    Title = "Friend request already exists",
                    Status = 400
                });
                return response;
            }
            var friendRequest = FriendRequest.Create(user.AppUserId, request.ReceiverId);

            _context.FriendRequests.Add(friendRequest);
            await _context.SaveChangesAsync(cancellationToken);

            response.Payload = true;
            return response;
        }
    }
}