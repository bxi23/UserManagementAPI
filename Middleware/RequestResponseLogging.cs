using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace UserManagementAPI_TechHiveSolutionsLab.Middleware
{
    public class RequestResponseLogging
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLogging> _logger;

        public RequestResponseLogging(RequestDelegate next, ILogger<RequestResponseLogging> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log the request
            _logger.LogInformation($"Incoming request: {context.Request.Method} {context.Request.Path}");

            // Copy original response body stream
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                // Call the next middleware in the pipeline
                await _next(context);

                // Log the response
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                _logger.LogInformation($"Outgoing response: {context.Response.StatusCode} {responseText}");

                // Copy the contents of the new memory stream (which contains the response) to the original stream
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
    }
}