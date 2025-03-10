using Microsoft.AspNetCore.Authorization;
using ShakSphere.Application.UseCases.AppUserProfile.Queries;
using System.Security.Claims;

namespace ShakSphere.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UserProfileController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        //[HttpPost]
        //[ModelValidation]
        //[Authorize(JwtBearerDefaults.AuthenticationScheme)]
        //public async Task<IActionResult> CreateUser([FromBody] CreateUserProfileRequestDTO user)
        //{
        //    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    var emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;

        //    if (userIdClaim == null)
        //    {
        //        return Unauthorized(new { message = "User ID is missing from the JWT token" });
        //    }
        //    var command = _mapper.Map<CreateUserCommand>(user);
        //    var response = await _mediator.Send(command);
        //    var userprofile = _mapper.Map<UserProfileResponseDTO>(response);

        //    return CreatedAtAction(nameof(GetUserById), new { Id = response.AppUserId }, userprofile);
        //}

        [HttpGet]
        [ModelValidation]
        //[Authorize(JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllProfiles()
        {
            var query = new GetAllUsersQuery();
            var users = await _mediator.Send(query);
            var usersDto = _mapper.Map<List<UserProfileResponseDTO>>(users);
            return Ok(usersDto);
        }

        [HttpGet("{Id}")]
        [ModelValidation]
        public async Task<IActionResult> GetUserById([FromRoute] string Id)
        {
            var query = new GetUserByIdQuery { UserId = Guid.Parse(Id) };
            var response = await _mediator.Send(query);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            var userprofile = _mapper.Map<UserProfileResponseDTO>(response.Payload);
            return Ok(userprofile);
        }
        [HttpPut("{Id}")]
        [ModelValidation]
        //[Authorize(JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateUser([FromRoute] string Id, [FromBody] UserProfileUpdateRequestDTO appUserDto) 
        {
            var command = _mapper.Map<UpdateUserCommand>(appUserDto);
            command.Id = Guid.Parse(Id);
            var response = await _mediator.Send(command);
            if (!response.Success) 
            {
                return BadRequest(response);
            }
            var updateduser = _mapper.Map<UserProfileResponseDTO>(response.Payload);
            return Ok(updateduser);
        }
        [HttpDelete("{Id}")]
        [ModelValidation]
        //[Authorize(JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteUser([FromRoute] string Id)
        {
            var command = new DeleteUserCommand();
            command.UserId = Guid.Parse(Id);
            var response = await _mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return NoContent();
        }
    }
}

