using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using ShakSphere.Domain.Aggregates.UserProfileAggregate;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace ShakSphere.Application.UseCases.AppUserProfile.Commands
{
    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResponseStatus<ApplicationUser>>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        private readonly UserManager<IdentityUser>  _userManager;

        public UpdateUserCommandHandler(IAppDbContext appDbContext, ILogger<UpdateUserCommandHandler> logger, UserManager<IdentityUser> userManager)
        {
            _context = appDbContext;
            _logger = logger;
            _userManager = userManager;
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
            using var transaction = _context.Database.BeginTransaction();

            try 
            {
                var info = BasicInfo.UpdateBasicInfo(request.FirstName, request.LastName, request.CurrentCity, request.Email);
                user.UpdateUserBasicInfo(info);
                _context.ApplicationUsers.Update(user);
                await _context.SaveChangesAsync(cancellationToken);
                var userUpdate = await _userManager.FindByEmailAsync(request.Email);
                if (userUpdate != null)
                {
                    userUpdate.Email = request.Email;
                    await _userManager.UpdateAsync(userUpdate);
                    await transaction.CommitAsync(cancellationToken);
                }
                else
                {
                    await transaction.RollbackAsync(cancellationToken);
                    response.Success = false;
                    response.Errors.Add(new ProblemDetails
                    {
                        Title = "Updateuser error",
                        Status = StatusCodes.Status404NotFound
                    });
                    return response;
                }
                    
            }
            catch (Exception ex) 
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }


            _logger.LogInformation($"User {request.Id} updated successfully");
            response.Payload = user;

            return response;
        }
    }
}
