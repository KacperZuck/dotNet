using Health_App.Common;
using Health_App.Data; 
using Microsoft.EntityFrameworkCore;

namespace Health_App.Common
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly ConfigDbContext _dbContext;

        public Repository(ConfigDbContext contex)
        {
            _dbContext = contex;
        }
        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity); 
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            await _dbContext.Set<T>().Where(x => x.id == id).ExecuteDeleteAsync();
        }

        public async Task<T?> Get(Guid id)
        {
            return await _dbContext.Set<T>().Where(x => x.id == id).FirstAsync();
        }

        public async Task<ICollection<T>?> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
