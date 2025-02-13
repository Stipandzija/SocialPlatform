using MediatR;
using ShakSphere.Application.Contracts.UserProfile.Request;
using ShakSphere.Application.Models;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;

namespace ShakSphere.Application.UseCases.AppUserProfile.Commands
{
    public class UpdateUserCommand : IRequest<ResponseStatus<ApplicationUser>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CurrentCity { get; set; }
    }
}
