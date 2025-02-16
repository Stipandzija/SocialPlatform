namespace ShakSphere.Domain.CustomExceptions
{
    public class PostCommentValidationException : Exception
    {
        public List<string> Errors { get; }
        public PostCommentValidationException() 
        {
            Errors = new List<string>();
        }
        public PostCommentValidationException(string message) : base(message) 
        {
            Errors = new List<string>();
        }
        public PostCommentValidationException(string message,Exception exception) : base(message,exception)
        {
            Errors = new List<string>();
        }
        public PostCommentValidationException(List<string> errors) : base("Post comment validation failed")
        {
            Errors = errors;
        }
    }
}
