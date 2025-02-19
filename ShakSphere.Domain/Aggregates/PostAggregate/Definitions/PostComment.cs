using ShakSphere.API.Aggregates.PostAggregate.Definitions;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;
using ShakSphere.Domain.AggregateValidator.PostValidators;
using ShakSphere.Domain.CustomExceptions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShakSphere.Domain.Aggregates.PostAggregate.Definitions
{
    public class PostComment
    {
        public Guid CommentId { get; private set; }
        public Guid PostId { get; private set; }
        public Guid AppUserId { get; private set; }
        public string Text { get; private set; }
        public DateTime DateOfCreation { get; private set; }
        public DateTime LastModified { get; private set; }

        private PostComment() { }
        public static PostComment CreatePostComment(Guid postId, string text, Guid userProfileId)
        {
            var validator = new PostCommentValidatoion();
            var postcomment = new PostComment
            {
                PostId = postId,
                Text = text,
                AppUserId = userProfileId,
                DateOfCreation = DateTime.Now,
                LastModified = DateTime.Now
            };
            var result = validator.Validate(postcomment);
            if (!result.IsValid)
            {
                var errors = new List<string>();
                foreach (var error in result.Errors)
                {
                    errors.Add($"{error.PropertyName}: {error.ErrorMessage}");
                }

                throw new PostCommentValidationException(errors);
            }
            return postcomment;
        }
    }

}
