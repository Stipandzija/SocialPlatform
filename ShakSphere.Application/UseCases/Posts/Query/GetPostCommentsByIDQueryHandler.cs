using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using ShakSphere.Domain.Aggregates.PostAggregate.Definitions;

namespace ShakSphere.Application.UseCases.Posts.Query
{
    public class GetPostCommentsByIDQueryHandler : IRequestHandler<GetPostCommentsByIDQuery, ResponseStatus<List<PostComment>>>
    {
        private readonly IAppDbContext _appDbContext;
        public GetPostCommentsByIDQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ResponseStatus<List<PostComment>>> Handle(GetPostCommentsByIDQuery request, CancellationToken cancellationToken)
        {
            var result = new ResponseStatus<List<PostComment>>();
            var post = await _appDbContext.Posts.Include(p=>p.Comments).FirstOrDefaultAsync(x => x.PostId == request.PostId, cancellationToken);
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
            result.Payload=post.Comments.ToList();
            return result;
        }

    }
}
