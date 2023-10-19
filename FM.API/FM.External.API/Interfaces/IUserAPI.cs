using FM.External.API.Models;

namespace FM.External.API.Interfaces
{
    /// <summary>
    /// Definición de métodos API Externa
    /// </summary>
    public interface IUserAPI
    {
        Task<UserResponse> GetUsersAsync(int page);
    }
}
