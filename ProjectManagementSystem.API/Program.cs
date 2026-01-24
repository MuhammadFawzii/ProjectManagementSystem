using ProjectManagementSystem.API.Extensions;
using ProjectManagementSystem.API.Middlewares;
using ProjectManagementSystem.Application.Extensions;
using ProjectManagementSystem.Infrastructure.Extensions;
using ProjectManagementSystem.Infrastructure.Seeders;
using Scalar.AspNetCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting web application");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.AddPresentation();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    var app = builder.Build();
    var scope=app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IProjectManagementSeeder>();
    await seeder.SeedAsync();
    Log.Information("Application built successfully");
    
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseMiddleware<RequestTimeLoggingMiddleware>();
    app.UseSerilogRequestLogging();
    
    // Enable Rate Limiting middleware
    app.UseRateLimiter();
    
    // Enable Output Caching middleware
    app.UseOutputCache();
        
    // JWT Authentication & Authorization middleware (order matters!)
    app.UseAuthentication();
    app.UseAuthorization();
    
    app.MapControllers();
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "ProjectManagementSystem.API v1");
            options.SwaggerEndpoint("/openapi/v2.json", "ProjectManagementSystem.API v2");
            options.EnableDeepLinking();
            options.DisplayRequestDuration();
            options.EnableFilter();

        });
        app.MapScalarApiReference();

    }

    Log.Information("Starting the host");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
    Console.WriteLine($"Fatal error: {ex.Message}");
    Console.WriteLine($"Stack trace: {ex.StackTrace}");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
