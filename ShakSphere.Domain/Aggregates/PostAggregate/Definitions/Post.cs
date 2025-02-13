using ShakSphere.Domain.Aggregates.PostAggregate.Definitions;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;

namespace ShakSphere.API.Aggregates.PostAggregate.Definitions
{
    public class Post
    {
        public Guid Postid { get; private set; }
        public Guid AppUserId { get; private set; }
        public ApplicationUser AppUser { get; private set; }
        public string TextContent { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModified { get; private set; }
        public List<PostComment> Comments { get; private set; }
        public List<PostInteraction> PostInteraction { get; private set; }
        private Post()
        {
            Comments = new List<PostComment>();
            PostInteraction = new List<PostInteraction>();
        }
        public static Post CreatePost(Guid appUserId, string textContext)
        {
            return new Post
            {
                AppUserId = appUserId,
                TextContent = textContext,
                CreatedDate = DateTime.Now,
                LastModified = DateTime.Now
            };
        }
        public void UpdatePost(string newtext) 
        {
            this.TextContent = newtext;
            this.LastModified = DateTime.Now;
        }
        public void AddPostComment(PostComment newComment) 
        {
            this.Comments.Add(newComment);
        }
        public void RemovePostComment(PostComment postComment) 
        {
            this.Comments.Remove(postComment);
        }
        public void AddInteraction(PostInteraction postInteraction)
        {
            this.PostInteraction.Add(postInteraction);
        }
        public void RemoveInteraction(PostInteraction postInteraction)
        {
            this.PostInteraction.Remove(postInteraction);
        }
    }
}
