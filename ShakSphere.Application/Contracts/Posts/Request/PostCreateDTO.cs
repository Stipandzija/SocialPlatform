using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;
using System.ComponentModel.DataAnnotations;

namespace ShakSphere.Application.Contracts.Posts.Request
{
    public class PostCreateDTO
    {
        [Required]
        public Guid AppUserId { get; set; } // TO DO dok ne napravim jwt token neman nacin da dodem do korinsika stoga stoji 
        [Required]
        public string TextContent { get; set; }
    }
}
