using Common;
using Health_App.Common;

namespace Health_App.Data
{
    public interface IUserRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {

    }
}
