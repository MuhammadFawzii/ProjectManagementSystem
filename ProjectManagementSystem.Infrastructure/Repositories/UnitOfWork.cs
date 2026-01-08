using ProjectManagementSystem.Domain.IRepositories;
using ProjectManagementSystem.Infrastructure.Persistence;
namespace ProjectManagementSystem.Infrastructure.Repositories;
internal class UnitOfWork(ProjectManagementSystemDbContext AppDbContext) : IUnitOfWork
{
    public void Dispose()
    {
        AppDbContext.Dispose();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        return new GenericRepository<TEntity>(AppDbContext);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return AppDbContext.SaveChangesAsync(cancellationToken);
    }
}
