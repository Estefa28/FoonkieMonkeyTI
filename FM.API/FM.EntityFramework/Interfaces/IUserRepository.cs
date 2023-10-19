using FM.Domain.Interfaces;
using FM.Domain.Models;

namespace FM.EntityFramework.Interfaces
{
    public interface IUserRepository: IRepository<UserEntity>
    {
    }
}
