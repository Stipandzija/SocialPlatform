using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using ShakSphere.Domain.Aggregates.UserProfileAggregate;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;
using Microsoft.AspNetCore.Mvc;

namespace ShakSphere.Application.UseCases.AppUserProfile.Commands
{
    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResponseStatus<ApplicationUser>>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<UpdateUserCommandHandler> _logger;

        public UpdateUserCommandHandler(IAppDbContext appDbContext, ILogger<UpdateUserCommandHandler> logger)
        {
            _context = appDbContext;
            _logger = logger;
        }

        public async Task<ResponseStatus<ApplicationUser>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating user {request.Id}");
            var response = new ResponseStatus<ApplicationUser>();
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.AppUserId == request.Id, cancellationToken);
            if (user == null)
            {
                _logger.LogWarning($"User with ID {request.Id} not found");

                response.Success = false;
                response.Errors.Add(new ProblemDetails
                {
                    Title = "User not found",
                    Status = StatusCodes.Status404NotFound
                });

                return response;
            }

            var info = BasicInfo.UpdateBasicInfo(request.FirstName, request.LastName, request.CurrentCity);
            user.UpdateUserBasicInfo(info);

            _context.ApplicationUsers.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"User {request.Id} updated successfully");
            response.Payload = user;

            return response;
        }
    }
}
