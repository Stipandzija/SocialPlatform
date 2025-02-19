using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using ShakSphere.Domain.Aggregates.PostAggregate.Definitions;
using System.Net;


namespace ShakSphere.Application.UseCases.Posts.Command
{
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, ResponseStatus<PostComment>>
    {
        private readonly IAppDbContext _appDbContext;
        public AddCommentCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<ResponseStatus<PostComment>> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus<PostComment>();
            var result = await _appDbContext.Posts.Where(x => x.AppUserId == request.UserId && x.PostId == request.PostId).FirstOrDefaultAsync(cancellationToken);

            if (result == null)
            {
                response.Success = false;
                response.Errors.Add(new ProblemDetails
                {
                    Title = "User or Comments are not valid",
                    Status = (int)HttpStatusCode.NotFound
                });
                return response;
            }
            var comment = PostComment.CreatePostComment(request.PostId, request.Comment, request.UserId);
            result.AddPostComment(comment);
            _appDbContext.Posts.Update(result);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            response.Payload = comment;
            return response;
        }
    }
}
