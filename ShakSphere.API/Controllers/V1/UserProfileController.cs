using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ShakSphere.Application.Contracts.UserProfile.Request;
using MediatR;
using ShakSphere.Application.UseCases.AppUserProfile.Commands;
using AutoMapper;
using ShakSphere.Application.Contracts.UserProfile.Response;
using ShakSphere.Application.UseCases.AppUserProfile.Queries;
using ShakSphere.API.Filters;
using ShakSphere.Application.Behaviors;
namespace ShakSphere.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserProfileController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UserProfileController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            var query = new GetAllUsersQuery();

            var users = await _mediator.Send(query);

            var usersDto = _mapper.Map<List<AppUserResponseDTO>>(users);

            return Ok(usersDto);
        }

        [HttpPost]
        [ModelValidation]
        public async Task<IActionResult> CreateUser([FromBody] AppUserCreateRequestDTO user)
        {
            var command = _mapper.Map<CreateUserCommand>(user);
            var response = await _mediator.Send(command);
            var userprofile = _mapper.Map<AppUserResponseDTO>(response);

            return CreatedAtAction(nameof(GetUserById), new { Id = response.AppUserId},userprofile);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string Id)
        {
            var query = new GetUserByIdQuery { UserId = Guid.Parse(Id) };
            var response = await _mediator.Send(query);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            var userprofile = _mapper.Map<AppUserResponseDTO>(response.Payload);
            return Ok(userprofile);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string Id, [FromBody] AppUserUpdateRequestDTO appUserDto) 
        {
            var command = _mapper.Map<UpdateUserCommand>(appUserDto);
            command.Id = Guid.Parse(Id);
            var response = await _mediator.Send(command);
            if (!response.Success) 
            {
                return BadRequest(response);
            }
            var updateduser = _mapper.Map<AppUserResponseDTO>(response.Payload);
            return Ok(updateduser);
        }
        [HttpDelete("{Id}")]
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

