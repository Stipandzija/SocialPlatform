using ShakSphere.Domain.AggregateValidator.PostValidators;
using ShakSphere.Domain.CustomExceptions;

namespace ShakSphere.Domain.Aggregates.PostAggregate.Definitions
{
    public class PostComment
    {
        public Guid CommentId { get; private set; }
        public Guid PostId { get; private set; }
        public string Text { get; private set; }
        public Guid UserProfileID { get; private set; }
        public DateTime DateOfCreation { get; private set; }
        public DateTime LastModified { get; private set; }

        private PostComment() { }

        //private PostComment(Guid commentId, Guid postId, string text, Guid userProfileId, DateTime dateOfCreation, DateTime lastModified)
        //{
        //    CommentId = commentId;
        //    PostId = postId;
        //    Text = text;
        //    UserProfileID = userProfileId;
        //    DateOfCreation = dateOfCreation;
        //    LastModified = lastModified;
        //}

        public static PostComment CreatePostComment(Guid postId, string text, Guid userProfileId)
        {
            var validator = new PostCommentValidatoion();
            var postcomment = new PostComment
            {
                PostId = postId,
                Text = text,
                UserProfileID = userProfileId,
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
