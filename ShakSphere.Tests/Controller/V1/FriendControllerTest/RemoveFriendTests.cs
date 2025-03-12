using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShakSphere.Application.Models;
using ShakSphere.Application.UseCases.FriendRequests.Command;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;
using System.Security.Claims;

namespace ShakSphere.Tests.Controller.V1.FriendControllerTest
{
    public class RemoveFriendTests
    {
        private readonly FriendController _controller;
        private readonly IMediator _fakeMediator;
        public RemoveFriendTests()
        {
            _fakeMediator = A.Fake<IMediator>();
            _controller = new FriendController(_fakeMediator);
        }
        private void SetUserContext(string userId)
        {
            var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            };
            var identity = new ClaimsIdentity(claims, "Authtest");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };
        }
        [Fact]
        public async Task FriendControllerTest_RemoveFriend_ReturnUnauthorized()
        {
            //Arrange
            var friendId = Guid.NewGuid();
            _controller.ControllerContext = new ControllerContext 
            {
                HttpContext = new DefaultHttpContext()
            };

            //Act
            var result = await _controller.RemoveFriend(friendId);

            //Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }
        [Fact]
        public async Task FriendControllerTest_RemoveFriend_ReturnNoContentAsync()
        {
            //Arrange
            var UserId = Guid.NewGuid();
            var Friend = Guid.NewGuid();

            SetUserContext(UserId.ToString());

            var response = new ResponseStatus<ApplicationUser> { Success = true };

            A.CallTo(() => _fakeMediator.Send(A<RemoveFriendCommand>._, A<CancellationToken>._)).Returns(response);
            //Act
            var result = await _controller.RemoveFriend(Friend);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task RemoveFriend_MediatorFail_ReturnBadRequest()
        {
            //Arrange
            var UserId = Guid.NewGuid();
            var FriendId = Guid.NewGuid();
            SetUserContext(UserId.ToString());
            var response = new ResponseStatus<ApplicationUser>
            {
                Success = false,
                Errors = new List<ProblemDetails> 
                { 
                    new ProblemDetails
                    { 
                        Title = "Error"
                    }
                }
            };
            A.CallTo(() => _fakeMediator.Send(A<RemoveFriendCommand>._, A<CancellationToken>._)).Returns(response);

            //Act
            var result = await _controller.RemoveFriend(FriendId);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
        [Fact]
        public async Task RemoveFriend_UserIdIsInvalid_ReturnUnauthorized()
        {
            //Arrange
            var UserId = "43535456";
            var Friend = Guid.NewGuid();

            SetUserContext(UserId);

            //Act
            var result = await _controller.RemoveFriend(Friend);

            //Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }
        [Fact]
        public async Task RemoveFriend_InvalidFriendId_ReturnBadRequest()
        {
            //Arrange
            var UserId = Guid.NewGuid();
            var invalidFriendId = Guid.Empty;

            SetUserContext(UserId.ToString());
            //Act
            var result = await _controller.RemoveFriend(invalidFriendId);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
        [Fact]
        public async Task RemoveFriend_FriendDoesNotExist_ReturnBadrequest()
        {
            // Arrange
            var UserId = Guid.NewGuid();
            var FriendId = Guid.NewGuid();

            SetUserContext(UserId.ToString());

            var response = new ResponseStatus<ApplicationUser>
            {
                Success = false,
                Errors = new List<ProblemDetails>
                {
                    new ProblemDetails
                    {
                        Detail="Friend not found"
                    }
                }
            };
            A.CallTo(() => _fakeMediator.Send(A<RemoveFriendCommand>._, A<CancellationToken>._)).Returns(response);

            // Act
            var result = await _controller.RemoveFriend(FriendId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
        [Fact]
        public async Task RemoveFriend_UserRemovesSelf_ReturnBadRequest()
        {
            //Arrange
            var UserId = Guid.NewGuid();

            SetUserContext(UserId.ToString());
            //Act
            var result = await _controller.RemoveFriend(UserId);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

    }
}