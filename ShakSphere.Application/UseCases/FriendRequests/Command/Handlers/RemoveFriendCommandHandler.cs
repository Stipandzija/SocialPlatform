using MediatR;
using Microsoft.EntityFrameworkCore;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ShakSphere.Application.UseCases.FriendRequests.Command.Handlers
{
    public class RemoveFriendCommandHandler : IRequestHandler<RemoveFriendCommand, ResponseStatus<ApplicationUser>>
    {
        private readonly IAppDbContext _appDbContext;
        public RemoveFriendCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<ResponseStatus<ApplicationUser>> Handle(RemoveFriendCommand request, CancellationToken cancellationToken)
        {
            var responseStatus = new ResponseStatus<ApplicationUser>();

            var user = await _appDbContext.ApplicationUsers.Include(x => x.Friends).FirstOrDefaultAsync(y => y.AppUserId == request.UserId,cancellationToken);
            if (user == null) 
            {
                responseStatus.Success = false;
                responseStatus.Errors.Add(new ProblemDetails
                {
                    Title = "Greska prilikom provjere Id-a.",
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = $"User pod Id-om: {request.UserId} nije pronaden."
                });
                return responseStatus;
            }
            var friend = user.Friends.FirstOrDefault(x => x.AppUserId == request.FriendId);//TO DO
            if (friend == null)
            {
                responseStatus.Success = false;
                responseStatus.Errors.Add(new ProblemDetails
                {
                    Title = "Prijatelj nije pronaden",
                    Status = (int)HttpStatusCode.NotFound
                });
                return responseStatus;
            }
            try
            {
                user.RemoveFriend(friend);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return responseStatus;
            }
            catch(Exception e)
            {
                responseStatus.Success = false;
                responseStatus.Errors.Add(new ProblemDetails
                {
                    Title = "Pogreska prilikom micanja prijatelja",
                    Status = (int)HttpStatusCode.BadRequest,
                    Detail = e.Message
                });
                return responseStatus;
            }

        }
    }
}
