using MediatR;
using ShakSphere.API.Aggregates.PostAggregate.Definitions;
using ShakSphere.Application.Models;

namespace ShakSphere.Application.UseCases.Posts.Command
{
    public class CreatePostCommand : IRequest<ResponseStatus<Post>>
    {
        public Guid UserId { get; set; }
        public string TextContent { get; set; }
    }
}
