using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShakSphere.Application.Models
{
    public class HandleExpection
    {
        public async Task<ProblemDetails> HandleExceptionAsync(HttpContext context, Exception exception) 
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = "An unexpected error occurred";

            switch (exception)
            {
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = "Unauthorized access";
                    break;

                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;

                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = "Resource not found";
                    break;

                default:
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            ProblemDetails problem = new()
            {
                Status = (int)statusCode,
                Title = message,
                Detail = exception.StackTrace,
                Instance = context.Request.Path,
            };
            return problem;
        }
    }
}
