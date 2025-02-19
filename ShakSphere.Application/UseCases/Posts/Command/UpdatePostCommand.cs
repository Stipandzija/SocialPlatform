using MediatR;
using ShakSphere.API.Aggregates.PostAggregate.Definitions;
using ShakSphere.Application.Models;

namespace ShakSphere.Application.UseCases.Posts.Command
{
    public class UpdatePostCommand : IRequest<ResponseStatus<Post>>
    {
        public Guid PostId { get; set; }
        public string text { get; set; }
        public Guid UserId { get; set; }
    }
}
