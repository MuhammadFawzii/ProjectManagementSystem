using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagementSystem.Domain.Entities;
namespace ProjectManagementSystem.Infrastructure.Configurations;
public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
{
       public void Configure(EntityTypeBuilder<ProjectTask> builder)
       {
              builder.HasKey(t => t.Id);
              builder.Property(t => t.Title)
                     .IsRequired()
                     .HasMaxLength(200);
              builder.Property(t => t.Description)
                     .HasMaxLength(1000);
              builder.Property(t => t.AssignedUserId)
                     .IsRequired();
              builder.Property(t => t.Status)
                     .HasConversion<string>() 
                     .IsRequired();
              builder.Property(t => t.CreatedAt)
                     .IsRequired();
       }
}