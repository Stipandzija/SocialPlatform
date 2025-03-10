using Microsoft.AspNetCore.Authorization;
using ShakSphere.Application.Contracts.FriendRequests;
using ShakSphere.Application.UseCases.FriendRequests.Command;
using ShakSphere.Application.UseCases.FriendRequests.Query;
using System.Security.Claims;

namespace ShakSphere.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FriendRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FriendRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("send")]
        [ModelValidation]
        [Authorize(JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SendFriendRequest([FromBody] SendFriendRequestRequestDTO request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();
           
            var checkQuery = new CheckFollowRequestStatusQuery(Guid.Parse(userId), request.ReceiverId);
            var existingRequest = await _mediator.Send(checkQuery);

            if (existingRequest.Exists)
                return BadRequest(new { message = "Follow request already exists." });
            var command = new SendFriendRequestCommand(Guid.Parse(userId), request.ReceiverId);
            var response = await _mediator.Send(command);

            if (response.Success)
                return Ok();
            else
                return BadRequest(response);
        }

        [HttpPost("accept")]
        [ModelValidation]
        [Authorize(JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AcceptFriendRequest([FromBody] AcceptFriendRequestRequestDTO request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var command = new AcceptFriendRequestCommand(Guid.Parse(userId), request.RequestId);
            var response = await _mediator.Send(command);

            if (response.Success)
                return Ok();
            else
                return NotFound(response);
        }

        [HttpPost("reject")]
        [ModelValidation]
        [Authorize(JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> RejectFriendRequest([FromBody] RejectFriendRequestRequestDTO request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var command = new RejectFriendRequestCommand(Guid.Parse(userId), request.RequestId);
            var response = await _mediator.Send(command);

            if (response.Success)
                return Ok();
            else
                return NotFound(response);
        }
        [HttpGet("CheckStatus")]
        [ModelValidation]
        [Authorize(JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CheckFollowRequestStatus([FromQuery] Guid targetUserId)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(currentUserId))
                return Unauthorized();

            var query = new CheckFollowRequestStatusQuery(Guid.Parse(currentUserId), targetUserId);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

    }
}
