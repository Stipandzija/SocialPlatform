using Microsoft.AspNetCore.Authorization;
using ShakSphere.Application.UseCases.FriendRequests.Query;
using System.Security.Claims;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class FriendController : ControllerBase
{
    private readonly IMediator _mediator;

    public FriendController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    [ModelValidation]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetFriends()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }
        var response = await _mediator.Send(new GetUserFriendsQuery(Guid.Parse(userId)));
        if (!response.Success) return NotFound(response);
        return Ok(response.Payload);
    }

    [HttpGet("FollowRequests")]
    [ModelValidation]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetFollowRequests()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedUserId))
            return Unauthorized();

        var result = await _mediator.Send(new GetFollowRequestsQuery(parsedUserId));

        if (!result.Success)
            return NotFound(result.Errors);

        return Ok(result.Payload);
    }
}