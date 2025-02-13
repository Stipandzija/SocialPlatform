using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShakSphere.Application.UseCases.AppUserProfile.Commands
{
    internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResponseStatus<ApplicationUser>>
    {
        public readonly IAppDbContext _context;
        public DeleteUserCommandHandler(IAppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<ResponseStatus<ApplicationUser>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus<ApplicationUser>();
            var profile = _context.ApplicationUsers.FirstOrDefault(x=>x.AppUserId == request.UserId);
            if (profile == null)
            {
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

            response.Payload = profile;
            return response;
        }
    }
}
