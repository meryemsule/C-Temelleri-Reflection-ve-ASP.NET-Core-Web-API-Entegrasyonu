using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var error = new
            {
                Message = "Sunucu tarafýnda beklenmeyen bir hata oluþtu.",
                Detail = context.Exception.Message,
                Timestamp = DateTime.UtcNow
            };

            Console.WriteLine($"[ApiExceptionFilter] Hata: {context.Exception}");

            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }
}