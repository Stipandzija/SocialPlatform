using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShakSphere.Domain.CustomExceptions;
using System.Diagnostics;
using System.Net;

namespace ShakSphere.Application.Models
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = "An unexpected error occurred";

            var problem = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = message,
                Detail = exception.StackTrace,
                Instance = context.Request.Path,
            };
            var traceId = context.TraceIdentifier;
            context.Response.Headers["X-Trace-Id"] = traceId;
            switch (exception)
            {
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    problem.Title = "Unauthorized access";
                    break;

                case ArgumentException argEx:
                    problem.Status = (int)HttpStatusCode.BadRequest;
                    problem.Title = argEx.Message;
                    break;

                case KeyNotFoundException:
                    problem.Status = (int)HttpStatusCode.NotFound;
                    problem.Title = "Resource not found";
                    break;

                case PostValidationException ex:
                    statusCode = HttpStatusCode.BadRequest;
                    problem.Title = "Post validation failed";
                    problem.Extensions.Add("errors", ex.Errors);
                    break;

                case PostCommentValidationException ex:
                    statusCode = HttpStatusCode.BadRequest;
                    problem.Title = "Postcomment validation failed";
                    problem.Extensions.Add("errors", ex.Errors);
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    problem.Extensions.Add("exception", exception.Message);
                    break;
            }

            problem.Extensions.Add("traceId", traceId); 

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(problem, cancellationToken);
            return true;
        }
    }
}
