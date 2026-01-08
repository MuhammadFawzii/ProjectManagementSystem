using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ProjectManagementSystem.API.Middlewares;
using ProjectManagementSystem.API.OpenApi.Transformers;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ProjectManagementSystem.API.Extensions;

/// <summary>
/// Extension methods for configuring the WebApplicationBuilder with presentation layer services.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddCustomApiVersioningServices();
        builder.Services.AddApiDocumentationServices();
        builder.Services.AddExceptionHandlingServices();
        builder.Services.AddControllerWithJsonConfigurationServices();
        builder.Host.UseSerilog((context, configuration)=> {
            configuration.ReadFrom.Configuration(context.Configuration);
        });
    }
    public static IServiceCollection AddCustomApiVersioningServices(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddMvc()
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
    public static IServiceCollection AddApiDocumentationServices(this IServiceCollection services)
    {
        string[] versions = new[] { "v1", "v2" };
        foreach (var version in versions)
        {
            services.AddOpenApi(version, op =>
            {
                //Versioning Congiguration
                op.AddDocumentTransformer<VersionInfoTransformer>();
                //Security Configuration 
                op.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
                op.AddOperationTransformer<BearerSecuritySchemeTransformer>();
            });
        }
        return services;
    }
    public static IServiceCollection AddExceptionHandlingServices(this IServiceCollection services)
    {
        services.AddScoped<ErrorHandlingMiddleware>();
        services.AddScoped<RequestTimeLoggingMiddleware>();
        return services;
    }
    public static IServiceCollection AddControllerWithJsonConfigurationServices(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Prevent circular reference errors
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());// Convert enums to strings in JSON
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;// Ignore null values in responses
            options.JsonSerializerOptions.PropertyNamingPolicy = null; // Use PascalCase
            options.JsonSerializerOptions.WriteIndented = false; // Minimize response size
        });
        return services;
    }
}


