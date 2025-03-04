using MediatR;
using ShakSphere.Application.Contracts.FriendRequests;
namespace ShakSphere.Application.UseCases.FriendRequests.Query
{
    public class CheckFollowRequestStatusQuery : IRequest<FollowRequestStatusDto>
    {
        public Guid SenderId { get; }
        public Guid ReceiverId { get; }

        public CheckFollowRequestStatusQuery(Guid senderId, Guid receiverId)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
        }
    }
}
