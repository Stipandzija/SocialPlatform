using MediatR;
using ShakSphere.API.Aggregates.PostAggregate.Definitions;
using ShakSphere.Application.Models;

namespace ShakSphere.Application.UseCases.Posts.Command
{
    public class DeletePostCommand : IRequest<ResponseStatus<Post>>
    {
        public Guid Id { get; set; }
    }
}
