using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;
using System;

namespace ShakSphere.Domain.Aggregates.FriendshipAggregate
{
    public enum FriendRequestStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    public class FriendRequest
    {
        public Guid RequestId { get; private set; }
        public Guid SenderId { get; private set; }
        public Guid ReceiverId { get; private set; }
        public FriendRequestStatus Status { get; private set; }
        public DateTime? DateSent { get; private set; }
        public DateTime? DateResponded { get; private set; }

        public ApplicationUser Sender { get; private set; }
        public ApplicationUser Receiver { get; private set; }

        private FriendRequest() { }

        public static FriendRequest Create(Guid senderId, Guid receiverId)
        {
            return new FriendRequest
            {
                RequestId = Guid.NewGuid(),
                SenderId = senderId,
                ReceiverId = receiverId,
                Status = FriendRequestStatus.Pending,
                DateSent = DateTime.Now
            };
        }
        public void Accept()
        {
            if (Status != FriendRequestStatus.Pending)
                throw new InvalidOperationException("Friend request is no longer pending.");

            Status = FriendRequestStatus.Accepted;
            DateResponded = DateTime.Now;
        }
        public void Reject()
        {
            if (Status != FriendRequestStatus.Pending)
                throw new InvalidOperationException("Friend request is no longer pending.");

            Status = FriendRequestStatus.Rejected;
            DateResponded = DateTime.Now;
        }
    }
}
