using LifePlanner.Server.Data;
using LifePlanner.Server.Services.Interfaces;
using LifePlanner.Server.Repositories.Interfaces;

namespace LifePlanner.Server.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        public GenericService(IGenericRepository<T> repository)
        {
             _repository = repository;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<T> GetById(int id)
        {
            return await _repository.GetById(id);
        }
        public async Task<T> Add(T entity)
        {
            return await _repository.Add(entity);
        }
        public async Task<T> Update(T entity)
        {
            return await _repository.Update(entity);
        }
        public async Task<T> Delete(int id)
        {
            return await _repository.Delete(id);
        }
    }
}
