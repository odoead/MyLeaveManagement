using Microsoft.Ajax.Utilities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace MyLeaveManagement.Contracts
{
    public interface IGenericRepositoryBase<T> where T : class
    {

        Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null
            );
        Task<T> GetAsync(Expression<Func<T, bool>> expression = null,
            List<string> includes = null
            );

        Task CreateAsync(T entity);
        void update(T entity);
        void delete(T entity);
        Task<bool> isExistsAsync(Expression<Func<T, bool>> expression = null);
    }
}
