using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShakSphere.API.Aggregates.PostAggregate.Definitions;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;

namespace ShakSphere.Application.UseCases.Posts.Query
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, ResponseStatus<Post>>
    {
        private readonly IAppDbContext _context;
        public GetPostByIdQueryHandler(IAppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<ResponseStatus<Post>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new ResponseStatus<Post>();
            var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(x => x.Postid == request.Id, cancellationToken);
            if (post == null) 
            {
                result.Success = false;
                result.Errors.Add(new ProblemDetails
                {
                    Title = "Post are empty",
                    Status = StatusCodes.Status404NotFound
                });
                return result;
            }
            result.Payload = post;
            return result;
        }
    }
}
