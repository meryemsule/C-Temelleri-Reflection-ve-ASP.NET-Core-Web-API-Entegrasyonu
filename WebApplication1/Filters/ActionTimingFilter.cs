using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace WebApplication1.Filters
{
    public class ActionTimingFilter : IActionFilter
    {
        private Stopwatch? _sw;
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _sw = Stopwatch.StartNew();
            var route = context.HttpContext.Request.Path;
            Console.WriteLine($"[ActionTiming] Baþladý: {route} - {DateTime.UtcNow:O}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _sw?.Stop();
            var elapsed = _sw?.ElapsedMilliseconds ?? 0;
            var route = context.HttpContext.Request.Path;
            Console.WriteLine($"[ActionTiming] Bitti: {route} - Süre: {elapsed} ms");
        }
    }
}