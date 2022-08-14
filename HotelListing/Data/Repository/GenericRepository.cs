using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelListing.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _db;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _db = context.Set<T>();
        }
        public async Task Add(T entity)
        {
            _db.Add(entity);
        }

        public async Task AddRange(T entities)
        {
            _db.AddRange(entities);
        }

        public async Task Delete(int Id)
        {
            var entity = await _db.FindAsync(Id);
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
             _db.RemoveRange(entities);
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? expression = null, List<string>? includes = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> query = _db;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (includes != null)
            {
                foreach (var prop in includes)
                {
                    query = query.Include(prop);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetById(Expression<Func<T, bool>>? expression = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;
            if(includes != null)
            {
                foreach(var prop in includes)
                {
                    query = query.Include(prop);
                }
            }

            return await query.FirstOrDefaultAsync(expression);
        }

        public void Update(T entity)
        {
            _db.Update(entity);
        }
    }
}
