using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShakSphere.Application.DataInterface;
using ShakSphere.Application.Models;
using ShakSphere.Domain.Aggregates.UserProfileAggregate;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;
namespace ShakSphere.Application.UseCases.Auth.Commands
{
    public class RegisterUserIdentityCommandHandler : IRequestHandler<RegisterUserIdentityCommand, ResponseStatus<string>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public RegisterUserIdentityCommandHandler(JwtTokenGenerator jwtTokenGenerator, IAppDbContext appDbContext, UserManager<IdentityUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ResponseStatus<string>> Handle(RegisterUserIdentityCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseStatus<string>();

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return GenerateErrorResponse("User or password is wrong,try again");
            }

            var user = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email
            };

            using var transaction = _appDbContext.Database.BeginTransaction();
            try
            {
                var creationResult = await CreateUserAsync(user, request.Password);
                user = creationResult.Payload;
                if (!creationResult.Success)
                {
                    //TO Do vratit reponse ne string
                    return GenerateErrorResponse("Cannot create user, try again"); ;
                }

                await SaveUserProfileAsync(user.Id, user.Email, cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }

            response.Payload = await _jwtTokenGenerator.GenerateJwtToken(user);
            return response;
        }

        private ResponseStatus<string> GenerateErrorResponse(string message)
        {
            return new ResponseStatus<string>
            {
                Success = false,
                Errors = { new ProblemDetails
                {
                    Title = message,
                    Status = StatusCodes.Status400BadRequest
                } }
            };
        }

        private async Task<ResponseStatus<IdentityUser>> CreateUserAsync(IdentityUser user, string password)
        {
            var response = new ResponseStatus<IdentityUser>();
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                response.Success = false;
                foreach (var error in result.Errors)
                {
                    response.Errors.Add(new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Detail = error.Description
                    });
                }
                return response;
            }
            response.Payload = user;
            return response;
        }

        private async Task SaveUserProfileAsync(string userId, string email, CancellationToken cancellationToken)
        {
            var basicInfo = BasicInfo.CreateBasicInfo("", "", DateTime.Now, "", email);
            var newUser = ApplicationUser.CreateUserProfile(userId, basicInfo);
            _appDbContext.ApplicationUsers.Add(newUser);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
