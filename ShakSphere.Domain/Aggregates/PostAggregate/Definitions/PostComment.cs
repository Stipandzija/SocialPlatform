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

        private PostComment(Guid commentId, Guid postId, string text, Guid userProfileId, DateTime dateOfCreation, DateTime lastModified)
        {
            CommentId = commentId;
            PostId = postId;
            Text = text;
            UserProfileID = userProfileId;
            DateOfCreation = dateOfCreation;
            LastModified = lastModified;
        }

        public static PostComment CreatePostComment(Guid postId, string text, Guid userProfileId)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Text is empty");
            }

            if (postId == Guid.Empty || userProfileId == Guid.Empty)
            {
                throw new ArgumentException("Id must be valid");
            }

            var commentId = Guid.NewGuid();
            var currentTime = DateTime.UtcNow;

            return new PostComment(commentId, postId, text, userProfileId, currentTime, currentTime);
        }
    }

}
