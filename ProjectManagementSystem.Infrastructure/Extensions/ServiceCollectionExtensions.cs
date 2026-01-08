using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProjectManagementSystem.Application.Common.Interfaces;
using ProjectManagementSystem.Domain.IRepositories;
using ProjectManagementSystem.Infrastructure.Authentication;
using ProjectManagementSystem.Infrastructure.Permissions;
using ProjectManagementSystem.Infrastructure.Persistence;
using ProjectManagementSystem.Infrastructure.Repositories;
using ProjectManagementSystem.Infrastructure.Seeders;
using System.Security;
using System.Text;

namespace ProjectManagementSystem.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDatabaseServices(configuration)
                .AddJwtAuthenticationServices(configuration)
                .AddAuthorizationPoliciesServices(configuration)
                .AddBusinessServices();
    }
    public static IServiceCollection AddJwtAuthenticationServices(this IServiceCollection services,IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]
                    ?? throw new InvalidOperationException("JWT SecretKey is not configured."))

                )
            };

        });
        return services;

    }
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITokenProvider, JwtTokenProvider>();
        services.AddScoped<IProjectManagementSeeder, ProjectManagementSeeder>();
        return services;
    }
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ProjectManagementSystemDb");
        services.AddDbContext<ProjectManagementSystemDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<IProjectManagementSeeder, ProjectManagementSeeder>();
        return services;
    }

    public static IServiceCollection AddAuthorizationPoliciesServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddAuthorization(options =>
        {
            // Project Management Permissions
            AddProjectPolicies(options);
            // Task Management Permissions
            AddTaskPolicies(options);
        });
        return services;
    }
    private static void AddProjectPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(Permission.Project.Create,
                   policy => policy.RequireClaim("permission", Permission.Project.Create));
        options.AddPolicy(Permission.Project.Read,
            policy => policy.RequireClaim("permission", Permission.Project.Read));
        options.AddPolicy(Permission.Project.Update,
            policy => policy.RequireClaim("permission", Permission.Project.Update));
        options.AddPolicy(Permission.Project.Delete,
            policy => policy.RequireClaim("permission", Permission.Project.Delete));
        options.AddPolicy(Permission.Project.ManageBudget,
            policy => policy.RequireClaim("permission", Permission.Project.ManageBudget));
    }
    private static void AddTaskPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(Permission.Task.Create,
            policy => policy.RequireClaim("permission", Permission.Task.Create));
        options.AddPolicy(Permission.Task.Read,
            policy => policy.RequireClaim("permission", Permission.Task.Read));
        options.AddPolicy(Permission.Task.Update,
            policy => policy.RequireClaim("permission", Permission.Task.Update));
        options.AddPolicy(Permission.Task.Delete,
            policy => policy.RequireClaim("permission", Permission.Task.Delete));
        options.AddPolicy(Permission.Task.AssignUser,
            policy => policy.RequireClaim("permission", Permission.Task.AssignUser));
        options.AddPolicy(Permission.Task.UpdateStatus,
            policy => policy.RequireClaim("permission", Permission.Task.UpdateStatus));
        options.AddPolicy(Permission.Task.Comment,
            policy => policy.RequireClaim("permission", Permission.Task.Comment));
    }
}
