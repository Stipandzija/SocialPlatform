using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakSphere.Application.Contracts.UserProfile.Request
{
    public class AppUserUpdateRequestDTO
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CurrentCity { get; set; }
    }
}
