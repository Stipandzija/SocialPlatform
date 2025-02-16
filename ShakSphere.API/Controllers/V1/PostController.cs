namespace ShakSphere.API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public PostController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPost()
        {
            var result = await _mediator.Send(new GetAllPostQuery());
            if (!result.Success)
            {
                return BadRequest(result);
            }
            var mapped = _mapper.Map<List<PostResponseDTO>>(result.Payload);
            return Ok(mapped);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPostById([FromRoute] string Id)
        {
            var query = new GetPostByIdQuery { Id = Guid.Parse(Id) };
            var result = await _mediator.Send(query);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            var mapped = _mapper.Map<PostResponseDTO>(result.Payload);
            return Ok(mapped);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostCreateDTO postCreateDTO)
        {
            var command = new CreatePostCommand()
            {
                UserId = Guid.Parse(postCreateDTO.AppUserId.ToString()),
                TextContent = postCreateDTO.TextContent
            };
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            var mapped = _mapper.Map<PostCreateDTO>(result.Payload);
            return CreatedAtAction(nameof(GetPostById), new { Id = mapped.AppUserId }, mapped);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdatePost([FromRoute] string Id,[FromBody] PostUpdateDTO postUpdateDTO) 
        {
            var command = new UpdatePostCommand()
            {
                PostId = Guid.Parse(Id),
                text = postUpdateDTO.TextContent
            };
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            var mapped = _mapper.Map<PostCreateDTO>(result.Payload);
            return CreatedAtAction(nameof(GetPostById), new { Id = mapped.AppUserId },mapped);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePost([FromRoute] string Id)
        {
            var command = new DeletePostCommand() { Id = Guid.Parse(Id) };
            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return NoContent();
        }

        [HttpGet("{postId}/comments")]
        public async Task<IActionResult> GetPostComments([FromRoute] string postId)
        {
            var query = new GetPostCommentsByIDQuery { PostId = Guid.Parse(postId) };
            var result = await _mediator.Send(query);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            var mappedComments = _mapper.Map<List<PostCommentResponseDTO>>(result.Payload);
            return Ok(mappedComments);
        }
        [HttpPost("{postId}/comments")]
        public async Task<IActionResult> AddComment([FromRoute] string postId, [FromBody] CreateCommentDTO createCommentDTO)
            // TO Do provjera guida unutar zahtjeva
        {
            if (!Guid.TryParse(createCommentDTO.UserId, out var UserId)) 
            {
                return BadRequest("Guid nije pravilnog formata");
            }

            var command = new AddCommentCommand
            {
                PostId = Guid.Parse(postId),
                Comment = createCommentDTO.Text,
                UserId = UserId
            };

            var result = await _mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            var mappedComment = _mapper.Map<PostCommentResponseDTO>(result.Payload);
            return Ok(mappedComment);
        }
        [HttpGet("{postId}/comments/{commentId}")]
        public async Task<IActionResult> updatecomment() 
        {
            return Ok();
        }
    }
}
