using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NetPC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace NetPC.Data
{
    public class Repository<T> : IRepository<T> where T : class,IEntity
    {
        private readonly DBContext context;
        public Repository(DBContext item) {
            context = item;
        }

        public async Task Create(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetbyId(Guid id)
        {
            return await context.Set<T>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(T entity)
        {
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var item = context.Set<T>()
                .FirstOrDefault(x => x.Id == id);
            
            if (item == null) 
                return;

            context.Set<T>().Remove(item);
            await context.SaveChangesAsync();
        }
    }
}
