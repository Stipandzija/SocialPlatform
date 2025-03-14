﻿using Microsoft.AspNetCore.Authorization;
using ShakSphere.Application.UseCases.FriendRequests.Command;
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

    [HttpGet("GetAll")]
    [ModelValidation]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetFriends()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
        var response = await _mediator.Send(new GetUserFriendsQuery(Guid.Parse(userId)));
        if (!response.Success) 
            return NotFound(response);
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
    [HttpPost("GetUserNamesByIds")]
    [ModelValidation]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetUserNamesByIds([FromBody] List<Guid> userIds)
    {
        if (userIds == null || !userIds.Any())
            return BadRequest();

        var query = new GetUsersByIdsQuery { UserIds = userIds };
        var response = await _mediator.Send(query);

        if (!response.Success || response.Payload == null)
            return NotFound(new List<object>());

        var userNames = response.Payload.Where(u => u.BasicInfo != null).Select(u =>
            new { Id = u.AppUserId, Name = u.BasicInfo.FirstName }).ToList();
        return Ok(userNames);
    }

    [HttpDelete("removeFriend")]
    [ModelValidation]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> RemoveFriend([FromBody] Guid friendId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var userGuidId))
            return Unauthorized();

        if (friendId == Guid.Empty)
            return BadRequest("Guid is empty");

        if (userGuidId == friendId)
            return BadRequest("Korisnik nemoze imat isti Id kao prijatelj");

        var command = new RemoveFriendCommand
        {
            UserId = userGuidId,
            FriendId = friendId
        };
        var response = await _mediator.Send(command);
        if(response.Success)
            return NoContent();
        return BadRequest(response.Errors);
    }
}