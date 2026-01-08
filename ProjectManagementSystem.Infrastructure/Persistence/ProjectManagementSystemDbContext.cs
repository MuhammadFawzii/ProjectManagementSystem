using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Infrastructure.Configurations;
namespace ProjectManagementSystem.Infrastructure.Persistence;
internal class ProjectManagementSystemDbContext(DbContextOptions<ProjectManagementSystemDbContext> options): DbContext(options)
{
    internal DbSet<Project> Projects { get; set; }
    internal DbSet<ProjectTask> ProjectTasks { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectTaskConfiguration).Assembly);
    }
}
