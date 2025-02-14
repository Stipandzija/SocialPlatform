using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;
using System.Net;

namespace ShakSphere.Application.UseCases.AppUserProfile.Queries
{
    internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ResponseStatus<ApplicationUser>>
    {
        private readonly IAppDbContext _context;
        public GetUserByIdQueryHandler(IAppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<ResponseStatus<ApplicationUser>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus<ApplicationUser>();
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.AppUserId == request.UserId, cancellationToken);
            if (user == null)
            {
                response.Success = false;
                response.Errors.Add(new ProblemDetails
                {
                    Title = "User not found",
                    Status = (int)HttpStatusCode.NotFound
                });
                return response;
            }
            response.Payload = user;
            return response;
        }
    }
}
