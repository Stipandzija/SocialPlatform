namespace ShakSphere.Application.Contracts.Posts.Response
{
    public class PostResponseDTO
    {
        public Guid PostId { get; set; }
        public string AppUserId { get; set; }
        public string TextContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public List<PostCommentResponseDTO> Comments { get; set; }
        public int InteractionCount { get; set; }
    }
}
