using MediatR;
using ShakSphere.Application.Models;

namespace ShakSphere.Application.UseCases.FriendRequests.Command
{
    public class RejectFriendRequestCommand : IRequest<ResponseStatus<bool>>
    {
        public Guid RequestId { get; }
        public Guid UserId { get; }
        public RejectFriendRequestCommand(Guid requestId, Guid userId)
        {
            RequestId = requestId;
            UserId = userId;
        }
    }
}
