using ShakSphere.Application.Contracts.UserProfile.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakSphere.Application.Contracts.UserProfile.Request
{
    public record AppUserCreateRequestDTO
    {
        //TO DO: nadopunit request
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? CurrentCity { get; set; }
    }
}
