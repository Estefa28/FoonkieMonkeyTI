using FM.Domain.Models;
using FM.EntityFramework.Interfaces;

namespace FM.EntityFramework.Repositories
{
    public class UserRepository : EFRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
