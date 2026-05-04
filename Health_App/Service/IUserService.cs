using Health_App.Common;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Health_App.Service
{
    public interface IUserService<TEntity, T> : IService<TEntity, T>
        where TEntity : IEntity
        where T : IDto
    {
        Task<T?> Login(string email, string pasword);
        Task Register(T newUser);
    }
}
