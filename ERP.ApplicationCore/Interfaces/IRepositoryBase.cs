using System.Linq.Expressions;

namespace ERP.ApplicationCore.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        // Advanced Get with filter, order, include, pagination
        Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            int? page = null,
            int? pageSize = null
        );

        Task<IEnumerable<T>> GetAsync(string includeProperties);
    }
}

