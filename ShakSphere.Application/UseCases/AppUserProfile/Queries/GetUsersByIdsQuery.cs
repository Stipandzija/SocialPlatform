using MediatR;
using ShakSphere.Application.Models;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;

namespace ShakSphere.Application.UseCases.AppUserProfile.Queries
{
    public class GetUsersByIdsQuery : IRequest<ResponseStatus<List<ApplicationUser>>>
    {
        public List<Guid> UserIds { get; set; } = new List<Guid>();
    }
}
