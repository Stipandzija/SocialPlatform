using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using ShakSphere.Domain.Aggregates.UserProfileAggregate;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;
using System.Net;


namespace ShakSphere.Application.UseCases.AppUserProfile.Commands
{
    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResponseStatus<ApplicationUser>>
    {
        private readonly IAppDbContext _context;
        private readonly HandleExpection _expection;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateUserCommandHandler(IAppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _expection = new HandleExpection();
            _context = appDbContext;
        }
        public async Task<ResponseStatus<ApplicationUser>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus<ApplicationUser>();
            try 
            {
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.AppUserId == request.Id);
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
                var info = BasicInfo.UpdateBasicInfo(request.FirstName, request.LastName, request.CurrentCity);

                user.UpdateUserBasicInfo(info);

                _context.ApplicationUsers.Update(user);
                await _context.SaveChangesAsync(cancellationToken);

                response.Payload = user;
            }
            catch (Exception e) 
            {
                Console.WriteLine(e);

                response.Success = false;

                var problemDetails = await _expection.HandleExceptionAsync(_httpContextAccessor.HttpContext, e);

                response.Errors.Add(problemDetails);

            }
            return response;
        }

    }
}
