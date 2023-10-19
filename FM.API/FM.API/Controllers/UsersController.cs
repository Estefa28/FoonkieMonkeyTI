using FM.Domain.Models;
using FM.EntityFramework.Interfaces;
using FM.External.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        
        public UsersController(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserEntity>>> GetAllUsersAsync(int page = 1, int pageSize = 5)
        {
            var response = _userRepository.GetAll(page, pageSize);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserEntity>> GetUserByIdAsync(int id)
        {
            var response = _userRepository.Search(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<UserEntity>> CreateUserAsync([FromBody] UserEntity request)
        {
            var response = _userRepository.Add(request);
            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<UserEntity>> UpdateUserAsync(int id, [FromBody] UserEntity request)
        {
            request.Id = id;
            var response = _userRepository.Update(request);
            return Ok(response);
        }
    }
}
