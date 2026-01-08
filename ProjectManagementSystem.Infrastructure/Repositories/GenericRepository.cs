using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Infrastructure.Persistence;
using ProjectManagementSystem.Domain.IRepositories;
using System.Linq.Expressions;
using ProjectManagementSystem.Domain.Constants;

namespace ProjectManagementSystem.Infrastructure.Repositories;

internal class GenericRepository<T>(ProjectManagementSystemDbContext AppDbContext) : IGenericRepository<T> where T : class
{
    protected readonly DbSet<T> _dbSet = AppDbContext.Set<T>();
    
    public void AddAsync(T entity)
    {
        _dbSet.Add(entity);
    }

    public void DeleteAsync(Guid id)
    {
        var entity = _dbSet.Find(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var entities = _dbSet.ToList().AsEnumerable();
        return await Task.FromResult(entities);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        var entity = _dbSet.Find(id);
        return await Task.FromResult(entity);
    }

    public async Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        
        return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
    }
    public async Task<(IEnumerable<T>, int)> GetAllMatchingAsync(
    string? searchPhrase,
    int pageSize,
    int pageNumber,
    string? sortBy,
    SortDirection sortDirection,
    Dictionary<string, Expression<Func<T, object>>> sortColumns,
    Expression<Func<T, string>>[] searchColumns,
    params Expression<Func<T, object>>[] includes
)
    {
        IQueryable<T> query = _dbSet.AsQueryable();

        // 🔗 INCLUDES
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        // 🔎 SEARCH
        if (!string.IsNullOrWhiteSpace(searchPhrase) && searchColumns.Any())
        {
            var searchLower = searchPhrase.ToLower();
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression? combined = null;

            foreach (var column in searchColumns)
            {
                var body = Expression.Invoke(column, parameter);

                var toLower = Expression.Call(
                    body,
                    nameof(string.ToLower),
                    Type.EmptyTypes);

                var contains = Expression.Call(
                    toLower,
                    nameof(string.Contains),
                    Type.EmptyTypes,
                    Expression.Constant(searchLower));

                combined = combined == null
                    ? contains
                    : Expression.OrElse(combined, contains);
            }

            var lambda = Expression.Lambda<Func<T, bool>>(combined!, parameter);
            query = query.Where(lambda);
        }

        // 🔢 TOTAL COUNT
        var totalCount = await query.CountAsync();

        // ↕ SORTING
        if (!string.IsNullOrEmpty(sortBy) && sortColumns.TryGetValue(sortBy, out var selectedColumn))
        {
            query = sortDirection == SortDirection.Ascending
                ? query.OrderBy(selectedColumn)
                : query.OrderByDescending(selectedColumn);
        }

        // 📄 PAGINATION
        var items = await query
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }


    public void UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
    }
}
