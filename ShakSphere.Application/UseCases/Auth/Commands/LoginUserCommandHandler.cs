using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using ShakSphere.Application.Security;
using System.IdentityModel.Tokens.Jwt;


namespace ShakSphere.Application.UseCases.Auth.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResponseStatus<string>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        public LoginUserCommandHandler(JwtTokenGenerator jwtTokenGenerator, IAppDbContext appDbContext, UserManager<IdentityUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<ResponseStatus<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus<string>();
            var existinguser = await _userManager.FindByEmailAsync(request.Email);
            if (existinguser == null)
            {
                response.Success = false;
                response.Errors.Add(new ProblemDetails
                {
                    Title = "Cannot login,wrong email or pasword",
                    Status = StatusCodes.Status400BadRequest
                });

                return response;
            }
            var passwordcheck = await _userManager.CheckPasswordAsync(existinguser, request.Password);
            if (!passwordcheck)
            {
                response.Success = false;
                response.Errors.Add(new ProblemDetails
                {
                    Title = "Cannot login,wrong email or pasword",
                    Status = StatusCodes.Status400BadRequest
                });

                return response;
            }


            response.Payload = await _jwtTokenGenerator.GenerateJwtToken(existinguser);
            return response;
        }
    }
}
