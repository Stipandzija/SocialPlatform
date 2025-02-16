using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShakSphere.API.Aggregates.PostAggregate.Definitions;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using System.Net;

namespace ShakSphere.Application.UseCases.Posts.Command
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, ResponseStatus<Post>>
    {
        private readonly IAppDbContext _appDbContext;
        public DeletePostCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

        }

        public async Task<ResponseStatus<Post>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus<Post>();
            var post = await _appDbContext.Posts.FirstOrDefaultAsync(x => x.Postid == request.Id, cancellationToken);
            if (post == null)
            {
                response.Success = false;
                response.Errors.Add(new ProblemDetails
                {
                    Title = "Post not found",
                    Status = (int)HttpStatusCode.NotFound
                });
                return response;
            }
            _appDbContext.Posts.Remove(post);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            response.Payload = post;

            return response;
        }
    }
}
