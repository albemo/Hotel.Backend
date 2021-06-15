using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Backend.Infrastructure.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public BaseRepository(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IList<T>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<T> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<T> AddAsync(T entity) => await _repository.AddAsync(entity);

        public async Task<T> UpdateAsync(T entity) => await _repository.UpdateAsync(entity);

        public async Task<bool> DeleteAsync(T entity)
        {
            await _repository.DeleteAsync(entity);

            return true;
        }

        public IQueryable<T> Query() => _repository.Query();

        public IQueryable<T> QueryNoTracking() => _repository.QueryNoTracking();
    }
}
