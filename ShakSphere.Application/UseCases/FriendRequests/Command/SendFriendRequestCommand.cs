using MediatR;
using ShakSphere.Application.Models;

namespace ShakSphere.Application.UseCases.FriendRequests.Command
{
    public class SendFriendRequestCommand : IRequest<ResponseStatus<bool>>
    {
        public Guid SenderId { get; }
        public Guid ReceiverId { get; }

        public SendFriendRequestCommand(Guid senderId, Guid receiverId)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
        }
    }
}
