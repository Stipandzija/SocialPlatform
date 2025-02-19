using MediatR;
using Microsoft.Extensions.Logging;
using ShakSphere.Application.DataInterface;
using ShakSphere.Domain.Aggregates.UserProfileAggregate;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;

namespace ShakSphere.Application.UseCases.AppUserProfile.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApplicationUser>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IAppDbContext appDbContext, ILogger<CreateUserCommandHandler> logger)
        {
            _context = appDbContext;
            _logger = logger;
        }

        public async Task<ApplicationUser> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creating new user profile");
            var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.DateOfBirth, request.CurrentCity,request.Email);
            var user = ApplicationUser.CreateUserProfile(Guid.NewGuid().ToString(), basicInfo);
            
            await _context.ApplicationUsers.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"User profile created successfully ID: {user.AppUserId}");
            return user;
        }
    }
}
