namespace ShakSphere.Application.Contracts.Posts.Response
{
    public class PostCommentResponseDTO
    {
        public Guid CommentId { get; set; }
        public Guid PostId { get; private set; }
        public string Text { get; private set; }
        public Guid UserProfileID { get; private set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime LastModified { get; set; }
    }
}
