using MediatR;
using ShakSphere.Application.Models;

namespace ShakSphere.Application.UseCases.Auth.Commands
{
    public class LoginUserCommand : IRequest<ResponseStatus<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
