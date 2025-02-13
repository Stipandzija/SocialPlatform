using AutoMapper;
using MediatR;
using ShakSphere.Application.DataInterface;
using ShakSphere.Domain.Aggregates.UserProfileAggregate;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;


namespace ShakSphere.Application.UseCases.AppUserProfile.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApplicationUser>
    {
        private readonly IAppDbContext _context;
        public CreateUserCommandHandler(IAppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<ApplicationUser> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var basicinfo = BasicInfo.CreateBasicInfo(request.FirstName,request.LastName,
                request.DateOfBirth,request.CurrentCity);
            var user = ApplicationUser.CreateUserProfile(Guid.NewGuid().ToString(), basicinfo);
            await _context.ApplicationUsers.AddAsync(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}
