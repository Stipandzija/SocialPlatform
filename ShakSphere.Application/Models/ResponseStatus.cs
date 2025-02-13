using Microsoft.AspNetCore.Mvc;

namespace ShakSphere.Application.Models
{
    public class ResponseStatus<T>
    {
        public T Payload { get; set; }
        public bool Success { get; set; } = true;
        public List<ProblemDetails> Errors { get; set; } = new List<ProblemDetails>();
    }
}
