using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShakSphere.Application.UseCases.AppUserProfile.Commands
{
    internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResponseStatus<ApplicationUser>>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(IAppDbContext appDbContext, ILogger<DeleteUserCommandHandler> logger)
        {
            _context = appDbContext;
            _logger = logger;
        }

        public async Task<ResponseStatus<ApplicationUser>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Delete user starts ID: {request.UserId}");

            var response = new ResponseStatus<ApplicationUser>();

            var profile = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.AppUserId == request.UserId, cancellationToken);
            if (profile == null)
            {
                _logger.LogWarning($"User with ID {request.UserId} not found");

                response.Success = false;
                response.Errors.Add(new ProblemDetails
                {
                    Title = "User not found",
                    Status = (int)HttpStatusCode.NotFound
                });

                return response;
            }

            _context.ApplicationUsers.Remove(profile);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"User {request.UserId} deleted successfully");

            response.Payload = profile;
            return response;
        }
    }
}
