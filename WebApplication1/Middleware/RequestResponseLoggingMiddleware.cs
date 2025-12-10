using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WebApplication1.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            var requestTime = DateTime.UtcNow;
            _logger.LogInformation("Request: {Method} {Path} at {Time}", context.Request.Method, context.Request.Path, requestTime);

            await _next(context);

            sw.Stop();
            _logger.LogInformation("Response: {StatusCode} for {Method} {Path} (Elapsed {Elapsed}ms)", context.Response.StatusCode, context.Request.Method, context.Request.Path, sw.ElapsedMilliseconds);
        }
    }
}