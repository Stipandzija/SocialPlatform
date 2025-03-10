using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;
using System.Net;

namespace ShakSphere.Application.UseCases.AppUserProfile.Queries
{
    public class GetUsersByIdsQueryHandler : IRequestHandler<GetUsersByIdsQuery, ResponseStatus<List<ApplicationUser>>>
    {
        private readonly IAppDbContext _context;

        public GetUsersByIdsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseStatus<List<ApplicationUser>>> Handle(GetUsersByIdsQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus<List<ApplicationUser>>();
            if (request.UserIds == null || !request.UserIds.Any())
            {
                response.Success = false;
                response.Errors.Add(new ProblemDetails
                {
                    Title = "Invalid request",
                    Detail = "The provided list of UserIds is empty or null",
                    Status = (int)HttpStatusCode.BadRequest
                });
                return response;
            }
            var users = await _context.ApplicationUsers.Where(u => request.UserIds.Contains(u.AppUserId)).Include(u => u.BasicInfo).ToListAsync(cancellationToken);
            if (users == null || !users.Any())
            {
                response.Success = false;
                response.Errors.Add(new ProblemDetails
                {
                    Title = "Users not found",
                    Detail = "Nijedan pronađeni korisnik pod danim idovima",
                    Status = (int)HttpStatusCode.NotFound
                });
                return response;
            }
            response.Payload = users;
            response.Success = true;
            return response;
        }
    }
}
