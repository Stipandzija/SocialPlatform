using MediatR;
using ShakSphere.Application.Contracts.FriendRequests;
using ShakSphere.Application.Models;

namespace ShakSphere.Application.UseCases.FriendRequests.Query
{
    public class GetUserFriendsQuery : IRequest<ResponseStatus<List<FriendDto>>>
    {
        public Guid UserId { get; }

        public GetUserFriendsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
