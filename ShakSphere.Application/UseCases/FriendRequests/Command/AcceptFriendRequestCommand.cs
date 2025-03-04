using MediatR;
using ShakSphere.Application.Models;

namespace ShakSphere.Application.UseCases.FriendRequests.Command
{
    public class AcceptFriendRequestCommand : IRequest<ResponseStatus<bool>>
    {
        public Guid RequestId { get; }
        public Guid UserId { get; set; }

        public AcceptFriendRequestCommand(Guid userId,Guid requestId)
        {
            UserId = userId;
            RequestId = requestId;
        }
    }
}
