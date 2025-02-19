using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShakSphere.API.Aggregates.PostAggregate.Definitions;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using System.Net;

namespace ShakSphere.Application.UseCases.Posts.Command
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, ResponseStatus<Post>>
    {
        private readonly IAppDbContext _appDbContext;
        public UpdatePostCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<ResponseStatus<Post>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {   
            var response = new ResponseStatus<Post>();
            var post = await _appDbContext.Posts.FirstOrDefaultAsync(x => x.PostId == request.PostId, cancellationToken);
            if (post == null || post.AppUserId!=request.UserId)//ukoliko je null odmah ce vratit bez sljedece provjere
            {
                response.Success = false;
                response.Errors.Add(new ProblemDetails
                {
                    Title = "Post Update somthing wentwrong, notauthorized or not valid post",
                    Status = (int)HttpStatusCode.NotFound
                });
                return response;
            }
            post.UpdatePost(request.text);
            _appDbContext.Posts.Update(post);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            response.Payload = post;
            return response;
        }
    }
}
