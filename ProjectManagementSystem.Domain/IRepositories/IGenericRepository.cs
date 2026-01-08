using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using ProjectManagementSystem.Domain.Constants;

namespace ProjectManagementSystem.Domain.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync();
        void AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(Guid id);
        Task<(IEnumerable<T>, int)> GetAllMatchingAsync(
            string? searchPhrase, 
            int pageSize, 
            int pageNumber, 
            string? sortBy, 
            SortDirection sortDirection, 
            Dictionary<string, Expression<Func<T, object>>> sortColumns, 
            Expression<Func<T, string>>[] searchColumns,
            params Expression<Func<T, object>>[] includes);
    }
}
