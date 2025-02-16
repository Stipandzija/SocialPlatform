using ShakSphere.Domain.Aggregates.PostAggregate.Definitions;
using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;
using ShakSphere.Domain.AggregateValidator.PostValidators;
using ShakSphere.Domain.CustomExceptions;

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
        public List<PostComment> Comments { get; private set; } = new();
        public List<PostInteraction> PostInteraction { get; private set; } = new();
        private Post() { }
        public static Post CreatePost(Guid appUserId, string textContext, ApplicationUser applicationUser)
        {
            var validation = new PostValidation();
            var post = new Post
            {
                Postid = Guid.NewGuid(),
                AppUser = applicationUser,
                AppUserId = appUserId,
                TextContent = textContext,
                CreatedDate = DateTime.Now,
                LastModified = DateTime.Now
            };
            var result = validation.Validate(post);
            if (!result.IsValid)
            {
                var errors = new List<string>();
                foreach (var error in result.Errors)
                {
                    errors.Add($"{error.PropertyName}: {error.ErrorMessage}");
                }

                throw new PostValidationException(errors);
            }
            return post;
        }
        public void UpdatePost(string newtext) 
        {
            this.TextContent = newtext;
            this.LastModified = DateTime.Now;
        }
        public void AddPostComment(PostComment newComment) 
        {
            Comments.Add(newComment);
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
