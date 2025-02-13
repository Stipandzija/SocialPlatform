using MediatR;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;


namespace ShakSphere.Application.UseCases.AppUserProfile.Queries
{
    public class GetAllUsersQuery : IRequest<List<ApplicationUser>>
    {

    }
}
