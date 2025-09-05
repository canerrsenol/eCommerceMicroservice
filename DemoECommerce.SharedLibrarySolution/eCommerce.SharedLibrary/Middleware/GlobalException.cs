using eCommerce.SharedLibrary.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace eCommerce.SharedLibrary.Middleware
{
    public class GlobalException(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            // Declare variables
            string message = "Sorry, internal server error occured";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string title = "Error";

            try
            {
                await next(context);

                // Check if Response is Too Many Requests // 429 status code.
                if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
                {
                    title = "Warning";
                    message = "Too many requests. Please try again later.";
                    statusCode = (int)context.Response.StatusCode;

                    await ModifyHeader(context, title, message, statusCode);
                }
                // If Response is UnAuthorized // 401 status code.
                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    title = "Alert";
                    message = "You are not authorized to access this resource.";
                    await ModifyHeader(context, title, message, context.Response.StatusCode);
                }

                // If Response is Forbidden // 403 status code.
                if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    title = "Out of Access";
                    message = "You are not allowed/required to access this resource.";
                    statusCode = StatusCodes.Status403Forbidden;
                    await ModifyHeader(context, title, message, context.Response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                // Log Original Exceptions /File, Debugger, Console
                LogException.LogExceptions(ex);

                // Check if Exception is Timeout Exception
                if (ex is TaskCanceledException || ex is TimeoutException)
                {
                    title = "Timeout";
                    message = "Request timed out. Please try again later.";
                    statusCode = StatusCodes.Status408RequestTimeout;
                }

                // If exception is caught.
                // If none of the exceptions match, return generic error message
                await ModifyHeader(context, title, message, statusCode);
            }
        }

        private async Task ModifyHeader(HttpContext context, string title, string message, int statusCode)
        {
            // display scary-free message to client
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails 
            { 
              Detail = message,
              Status = statusCode,
              Title = title
            }), CancellationToken.None);

            return;
        }
    }
}
