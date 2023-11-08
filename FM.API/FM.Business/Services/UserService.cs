using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserService(IUserAPI userAPI, IUserRepository userRepository, IMapper mapper)
        {
            _userAPI = userAPI;
            _userRepository = userRepository;
            _mapper = mapper;
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
                _userRepository.Add(_mapper.Map<UserEntity>(item));
            }
        }
    }
}
