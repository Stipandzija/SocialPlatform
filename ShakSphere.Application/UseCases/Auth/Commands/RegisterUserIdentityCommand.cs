using MediatR;
using ShakSphere.Application.Models;

namespace ShakSphere.Application.UseCases.Auth.Commands
{
    public class RegisterUserIdentityCommand : IRequest<ResponseStatus<string>>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
