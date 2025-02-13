using MediatR;
using ShakSphere.Application.Models;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakSphere.Application.UseCases.AppUserProfile.Commands
{
    public class DeleteUserCommand : IRequest<ResponseStatus<ApplicationUser>>
    {
        public Guid UserId { get; set; }
    }
}
