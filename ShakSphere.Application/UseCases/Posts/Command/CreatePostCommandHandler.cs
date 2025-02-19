using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShakSphere.API.Aggregates.PostAggregate.Definitions;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using System.Net;

namespace ShakSphere.Application.UseCases.Posts.Command
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ResponseStatus<Post>>
    {
        private readonly IAppDbContext _appDbContext;
        public CreatePostCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<ResponseStatus<Post>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus<Post>();
            var user = await _appDbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.IdentityId == request.UserId.ToString(), cancellationToken);
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
            var post = Post.CreatePost(request.UserId, request.TextContent,user);
            await _appDbContext.Posts.AddAsync(post, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            //if(post==null) nepotrebn jer validacija baza inzimku ukoliko je sve dobro daje rezultat
            response.Payload = post;
            return response;
        }
    }
}
