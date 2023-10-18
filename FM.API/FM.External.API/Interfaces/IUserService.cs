using FM.External.API.Models;

namespace FM.External.API.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> GetUsersAsync(int page);
    }
}
