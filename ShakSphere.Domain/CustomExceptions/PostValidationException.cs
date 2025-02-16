namespace ShakSphere.Domain.CustomExceptions
{
    public class PostValidationException : Exception
    {
        public List<string> Errors { get; }

        public PostValidationException(): base("Post validation failed")
        {
            Errors = new List<string>();
        }

        public PostValidationException(string message): base(message)
        {
            Errors = new List<string>();
        }

        public PostValidationException(string message, Exception exception): base(message, exception)
        {
            Errors = new List<string>();
        }

        public PostValidationException(List<string> errors): base("Post validation failed")
        {
            Errors = errors;
        }
    }
}
