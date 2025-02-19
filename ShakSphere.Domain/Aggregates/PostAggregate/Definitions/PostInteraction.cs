using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShakSphere.API.Aggregates.PostAggregate.Definitions;

namespace ShakSphere.Domain.Aggregates.PostAggregate.Definitions
{
    public class PostInteraction
    {
        public Guid InteractionId { get; private set; }
        public Guid PostId { get; private set; }
        public EmotionType Emotions { get; private set; }
        private PostInteraction(Guid interactionId, Guid postId, EmotionType emotions)
        {
            InteractionId = interactionId;
            PostId = postId;
            Emotions = emotions;
        }
        public static PostInteraction CreatePostInteraction(Guid postId, EmotionType emotions)
        {
            if (postId == Guid.Empty)
            {
                throw new ArgumentException("ID Post is empty");
            }

            var interactionId = Guid.NewGuid();

            return new PostInteraction(interactionId, postId, emotions);
        }
    }
}
