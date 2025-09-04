using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.SharedLibrary.Middleware
{
    public class GlobalException(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            // Declare variables
            string message = "sorry, internal server error occured";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string title = "Error";

            try
            {
                await next(context);

                // Check if Exception is Too Many Requests // 429 status code.
                if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
                {
                    title = "Warning";
                    message = "Too many requests. Please try again later.";
                    statusCode = (int)context.Response.StatusCode;

                    await ModifyHeader(context, title, message, statusCode);
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)

            }
        }

        private async Task ModifyHeader(HttpContext context, string title, string message, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            var result = System.Text.Json.JsonSerializer.Serialize(new { title = title, message = message, statusCode = statusCode });
            await context.Response.WriteAsync(result);
        }
    }
}
