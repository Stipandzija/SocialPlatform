using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShakSphere.API.Aggregates.PostAggregate.Definitions;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;

namespace ShakSphere.Application.UseCases.Posts.Query
{
    public class GetAllPostQueryHandler : IRequestHandler<GetAllPostQuery, ResponseStatus<List<Post>>>
    {
        private readonly IAppDbContext _context;
        public GetAllPostQueryHandler(IAppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<ResponseStatus<List<Post>>> Handle(GetAllPostQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus<List<Post>>();
            if (!response.Success) 
            {
                response.Success = false;
                response.Errors.Add(new ProblemDetails
                {
                    Title = "GetAllPosts error",
                    Status = StatusCodes.Status404NotFound
                });

                return response;
            }
            var posts = await _context.Posts.Include(p=>p.Comments).ToListAsync(cancellationToken);
            response.Payload = posts;
            return response;
        }
    }
}
