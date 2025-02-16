using ShakSphere.Application.Behaviors.Behaviors;

using System.ComponentModel.DataAnnotations;

namespace ShakSphere.Application.Contracts.Posts.Request
{
    public class CreateCommentDTO
    {
        public string Text { get; set; }

        [Required]
        [GuidValidation(ErrorMessage = "GUID nije validan")]
        public string UserId { get; set; }
    }
}
