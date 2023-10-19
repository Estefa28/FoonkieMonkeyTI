using FM.API.Configurations;
using FM.API.Filters;
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
        private readonly IUserAPI _userApi;
        private readonly IUserRepository _userRepository;
        private Auth _auth;
        
        public UsersController(IUserAPI userService, IUserRepository userRepository, IOptions<Auth> auth)
        {
            _userApi = userService;
            _userRepository = userRepository;
            _auth = auth.Value;
        }

        /// <summary>
        /// Obtener lista de todos los Usuarios registrados.
        /// </summary>
        /// <param name="page">Número de la pagina a solicitar</param>
        /// <param name="pageSize">Cantidad de registros por pagina</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<UserEntity>>> GetAllUsersAsync(int page = 1, int pageSize = 5)
        {
            var response = _userRepository.GetAll(page, pageSize);
            return Ok(response);
        }

        /// <summary>
        /// Obtener datos del Usuario registrado filtrando por Id. 
        /// </summary>
        /// <param name="id">Id del Usuario</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ClientAuth]
        public async Task<ActionResult<UserEntity>> GetUserByIdAsync(int id)
        {
            var response = _userRepository.Search(id);
            return Ok(response);
        }

        /// <summary>
        /// Crear nuevo usuario
        /// </summary>
        /// <param name="request">Información del Usuario a crear</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserEntity>> CreateUserAsync([FromBody] UserEntity request)
        {
            var response = _userRepository.Add(request);
            return Ok(response);
        }

        /// <summary>
        /// Modificar datos de un Usuario existente, filtro por el Id. 
        /// </summary>
        /// <param name="id">Id del Usuario</param>
        /// <param name="request">Datos del Usuario a modificar</param>
        /// <returns></returns>
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
