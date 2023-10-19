using FM.Business.Interfaces;
using FM.Domain.Models;
using FM.EntityFramework.Interfaces;
using FM.External.API.Interfaces;

namespace FM.Business.Services
{
    public class UserService: IUserService
    {
        private readonly IUserAPI _userAPI;
        private readonly IUserRepository _userRepository;

        public UserService(IUserAPI userAPI, IUserRepository userRepository)
        {
            _userAPI = userAPI;
            _userRepository = userRepository;
        }

        public async Task UpdateDatabaseAsync(int page)
        {
            var dataApiExterna = await _userAPI.GetUsersAsync(page);

            if (page > dataApiExterna.TotalPages)
            {
                throw new Exception("No hay más registros para consultar");
            }
            
            foreach (var item in dataApiExterna.Users)
            {
                var newUser = new UserEntity();
                newUser.FirstName = item.FirstName;
                newUser.LastName = item.LastName;   
                newUser.Email = item.Email;
                newUser.Avatar = item.Avatar;

                _userRepository.Add(newUser);
            }
        }
    }
}
