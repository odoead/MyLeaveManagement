using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyLeaveManagement.Contracts;
using MyLeaveManagement.Data;
using System.Linq.Expressions;

namespace MyLeaveManagement.Repository
{
    public class GenericRepository<T> : IGenericRepositoryBase<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _db;
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _db = _context.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await _db.AddAsync(entity);
        }

        public void delete(T entity)
        {
            _db.Remove(entity);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _db;
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query.Include(item);
                }
            }
            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null
            )
        {
            IQueryable<T> query = _db;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public async Task<bool> isExistsAsync(Expression<Func<T, bool>> expression = null)
        {
            IQueryable<T> query = _db;
           return  await query.AnyAsync(expression);
        }



        public void update(T entity)
        {
            _db.Update(entity);

        }
    }
}
