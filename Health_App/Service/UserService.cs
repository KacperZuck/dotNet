using AutoMapper;
using Common;
using Health_App.Common;
using Health_App.Data;
using Health_App.Models;
namespace Health_App.Service
{
    public class UserService<TEntity, TDto> : ClassService<TEntity, TDto>, IUserService<TEntity, TDto>
        where TEntity : User
        where TDto : UserDto
    {
        private readonly IMapper _mapper;

        public UserService(IUserRepository<TEntity> userRepository, IMapper m)
            : base((Repository<TEntity>)userRepository, m) {
            _mapper = m;
        } 
       
        public async Task<TDto?> Login(string email, string pasword)
        {
            var users = await GetAll();
            var user = users.FirstOrDefault(x =>
                    x.email == email &&
                    x.pasword == pasword);

            return user;
        }

        public async Task Register(TDto newUser)
        {
            await Add(newUser);
        }

    }
}
