using FM.External.API.Models;

namespace FM.External.API.Interfaces
{
    /// <summary>
    ///  Definition of External API methods
    /// </summary>
    public interface IUserAPI
    {
        Task<UserResponse> GetUsersAsync(int page);
    }
}
