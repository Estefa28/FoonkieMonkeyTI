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
            var dataExternalApi = await _userAPI.GetUsersAsync(page);

            if (page > dataExternalApi.TotalPages)
            {
                throw new Exception("There are no more records to consult");
            }
            
            foreach (var item in dataExternalApi.Users)
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
