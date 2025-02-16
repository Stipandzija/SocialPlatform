using MediatR;
using ShakSphere.Application.Models;
using ShakSphere.Application.UseCases.Posts.Query;
using ShakSphere.Domain.Aggregates.PostAggregate.Definitions;

namespace ShakSphere.Application.UseCases.Posts.Command
{
    public class AddCommentCommand : IRequest<ResponseStatus<PostComment>>
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public string Comment { get; set; }
    }
}
