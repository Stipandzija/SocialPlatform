using MediatR;
using ShakSphere.Application.Models;
using ShakSphere.Domain.Aggregates.PostAggregate.Definitions;

namespace ShakSphere.Application.UseCases.Posts.Query
{
    public class GetPostCommentsByIDQuery : IRequest<ResponseStatus<List<PostComment>>>
    {
        public Guid PostId { get; set; }
    }
}
