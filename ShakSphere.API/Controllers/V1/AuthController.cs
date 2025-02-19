using ShakSphere.Application.Contracts.Identity;
using ShakSphere.Application.UseCases.Auth.Commands;

namespace ShakSphere.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("Registration")]
        [ModelValidation]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO) 
        {
            var command = new RegisterUserIdentityCommand
            {
                Username = registerDTO.Username,
                Password = registerDTO.Password,
                Email = registerDTO.Email,
            };
            var result = await _mediator.Send(command);
            if(!result.Success)
                return BadRequest(result.Errors);

            var authToken = new AuthResult()
            {
                Token = result.Payload
            };

            return Ok(authToken);
        }

        [HttpPost("Login")]
        [ModelValidation]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var command = new LoginUserCommand
            {
                Email = loginDTO.Email,
                Password = loginDTO.Password,
            };
            var result = await _mediator.Send(command);
            if (!result.Success)
                return BadRequest(result.Errors);
            var authToken = new AuthResult()
            {
                Token = result.Payload
            };
            return Ok(authToken);
        }
    }
}
