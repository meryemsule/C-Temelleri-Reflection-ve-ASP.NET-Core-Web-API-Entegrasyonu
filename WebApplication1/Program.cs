using WebApplication1.Filters;
using WebApplication1.Middleware;
AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
{
    Console.WriteLine("Reflection Load Error -> " + eventArgs.Exception.Message);
};

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ActionTimingFilter>();
    options.Filters.Add<ApiExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
