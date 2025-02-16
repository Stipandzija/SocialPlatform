using MediatR;
using ShakSphere.API.Aggregates.PostAggregate.Definitions;
using ShakSphere.Application.Behaviors;
using ShakSphere.Application.Models;

namespace ShakSphere.Application.UseCases.Posts.Query
{
    public class GetAllPostQuery: IRequest<ResponseStatus<List<Post>>> , INoValidationRequired
    {
    }
}
