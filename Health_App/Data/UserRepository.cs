using Common;
using Health_App.Common;
using Health_App.Data;
using Health_App.Models;

namespace Health_App.Data
{
    public class UserRepository : Repository<User>, IUserRepository<User>
    {
        public UserRepository(ConfigDbContext context) : base(context)
        {
        }       
    }
}
