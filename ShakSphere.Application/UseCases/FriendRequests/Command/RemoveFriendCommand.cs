using MediatR;
using ShakSphere.Application.Models;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;

namespace ShakSphere.Application.UseCases.FriendRequests.Command
{
    public class RemoveFriendCommand : IRequest<ResponseStatus<ApplicationUser>>
    {
        public Guid UserId { get; set;}
        public Guid FriendId { get; set;}
    }
}
