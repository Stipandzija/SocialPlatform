using MediatR;
using ShakSphere.Application.Models;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;

namespace ShakSphere.Application.UseCases.AppUserProfile.Queries
{
    public class GetUserByIdQuery : IRequest<ResponseStatus<ApplicationUser>>
    {
        public Guid UserId { get; set; }
    }
}
