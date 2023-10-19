using FM.API.Configurations;
using FM.Domain.Models;
using FM.EntityFramework.Interfaces;
using FM.External.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly Auth _auth;
        
        public UsersController(IUserService userService, IUserRepository userRepository, IOptions<Auth> auth)
        {
            _userService = userService;
            _userRepository = userRepository;
            _auth = auth.Value;
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
            var headers = Request.Headers;
            if (headers.ContainsKey("client_id") && headers.ContainsKey("client_secret"))
            {
                if (_auth.ClientId == headers["client_id"] && _auth.ClientSecret == headers["client_secret"])
                {
                    var response = _userRepository.Search(id);
                    return Ok(response);
                }
            }
            return Unauthorized("No tiene autorización"); 
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
