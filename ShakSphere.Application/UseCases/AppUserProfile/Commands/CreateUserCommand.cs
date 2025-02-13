

using MediatR;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;

namespace ShakSphere.Application.UseCases.AppUserProfile.Commands
{
    public class CreateUserCommand : IRequest<ApplicationUser>
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; } 
        public string? CurrentCity { get; set; }
    }
}
