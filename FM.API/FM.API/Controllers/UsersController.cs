using FM.API.Filters;
using FM.Domain.Models;
using FM.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get list of all registered Users
        /// </summary>
        /// <param name="page">Page number to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<UserEntity>> GetAllUsersAsync(int page = 1, int pageSize = 5)
        {
            var response = _userRepository.GetAll(page, pageSize);
            return Ok(response);
        }

        /// <summary>
        /// Get data from the registered User by filtering by Id 
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ClientAuth]
        public ActionResult<UserEntity> GetUserByIdAsync(int id)
        {
            var response = _userRepository.Search(id);
            return Ok(response);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="request">User information to create</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<UserEntity> CreateUserAsync([FromBody] UserEntity request)
        {
            var response = _userRepository.Add(request);
            return Ok(response);
        }

        /// <summary>
        /// Modify data of an existing User, filter by Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <param name="request">User data to modify</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public ActionResult<UserEntity> UpdateUserAsync(int id, [FromBody] UserEntity request)
        {
            request.Id = id;
            var response = _userRepository.Update(request);
            return Ok(response);
        }
    }
}
