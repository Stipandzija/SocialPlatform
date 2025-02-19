using System.ComponentModel.DataAnnotations;

namespace ShakSphere.Application.Contracts.UserProfile.Request
{
    public class UserProfileUpdateRequestDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CurrentCity { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }
}
