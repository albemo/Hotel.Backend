using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Backend.Infrastructure.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<IList<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(T entity);

        IQueryable<T> Query();

        IQueryable<T> QueryNoTracking();
    }
}
