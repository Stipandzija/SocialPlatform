using MediatR;
using ShakSphere.API.Aggregates.PostAggregate.Definitions;
using ShakSphere.Application.Models;

namespace ShakSphere.Application.UseCases.Posts.Query
{
    public class GetPostByIdQuery : IRequest<ResponseStatus<Post>>
    {
        public Guid Id { get; set; }
    }
}
