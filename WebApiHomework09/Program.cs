using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication1.Filters;
using WebApplication1.Middleware;
using System;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    // Global filtreler
    options.Filters.Add<ActionTimingFilter>(); // Action timing
    options.Filters.Add<ApiExceptionFilter>(); // Global exception handling (controller/action seviyesinde)
});

// Swagger ekleyebilirsiniz (dev için)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Diagnostic: list loaded OpenAPI/Swashbuckle-related assemblies to detect version conflicts
try
{
    var relevant = AppDomain.CurrentDomain.GetAssemblies()
        .Select(a => new
        {
            Name = a.GetName().Name,
            Version = a.GetName().Version?.ToString() ?? "<no-version>",
            Location = a.IsDynamic ? "<dynamic>" : (string.IsNullOrEmpty(a.Location) ? "<no-location>" : a.Location)
        })
        .Where(x => x.Name?.IndexOf("OpenApi", StringComparison.OrdinalIgnoreCase) >= 0
                 || x.Name?.IndexOf("Swashbuckle", StringComparison.OrdinalIgnoreCase) >= 0
                 || x.Name?.IndexOf("Swagger", StringComparison.OrdinalIgnoreCase) >= 0)
        .OrderBy(x => x.Name)
        .ToList();

    Console.WriteLine("=== Loaded OpenAPI/Swashbuckle-related assemblies ===");
    foreach (var a in relevant)
    {
        Console.WriteLine($"Loaded: {a.Name} v{a.Version} @ {a.Location}");
    }
    Console.WriteLine("=====================================================");
}
catch (Exception ex)
{
    Console.WriteLine("Assembly-dump failed: " + ex.Message);
}

// Middleware sýralamasý önemlidir
app.UseMiddleware<RequestResponseLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();