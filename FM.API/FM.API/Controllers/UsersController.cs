using FM.Domain.Models;
using FM.External.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserEntity>>> GetAllUsersAsync(int page, int pageSize = 5)
        {
            var response = await _userService.GetUsersAsync(page);
            var list = response.Users;
            return Ok(list.Skip(page*pageSize-pageSize).Take(pageSize));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserEntity>> GetUserByIdAsync(int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<UserEntity>> CreateUserAsync([FromBody] UserEntity request)
        {
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<UserEntity>> UpdateUserAsync(int id, [FromBody] UserEntity request)
        {
            return Ok();
        }
    }
}
