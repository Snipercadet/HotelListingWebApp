using System.Linq.Expressions;

namespace HotelListing.Data.Repository
{
    public interface IGenericRepository<T> where T: class
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>>? expression = null, 
            List<string>? includes = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy=null);

        Task<T> Get(Expression<Func<T, bool>>? expression= null, List<string> includes= null);
        Task Add(T entity);
        Task AddRange(T entities);
        Task Delete(int Id);
        void DeleteRange(IEnumerable<T> entities);
        void Update(T entity);
    }
}
