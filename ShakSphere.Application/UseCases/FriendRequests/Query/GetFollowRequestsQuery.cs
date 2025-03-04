using MediatR;
using ShakSphere.Application.Contracts.FriendRequests;
using ShakSphere.Application.Models;

public class GetFollowRequestsQuery : IRequest<ResponseStatus<List<FriendRequestDto>>>
{
    public Guid UserId { get; }

    public GetFollowRequestsQuery(Guid userId)
    {
        UserId = userId;
    }
}
