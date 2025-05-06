using System.Net;
using ApiPeliculas.Models;
using APISINEB.Models.Dtos;
using APISINEB.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISINEB.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _usRepo;
        private readonly IMapper _mapper;
        protected ApiResponse _responseApi; //llamamos la clase de respuestas

        public UsersController(IUserRepository usRepo, IMapper mapper)
        {
            _usRepo = usRepo;
            _mapper = mapper;
            this._responseApi = new(); //asi se accede a las respuestas de ResponseApi
        }

        //[Authorize] //autorización por roles
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsers()
        {
            var listUsers = _usRepo.GetUsers();

            var listUsersDto = new List<UserDto>();
            foreach (var lista in listUsers)
            {
                //pasar de lista usuarios a listUserDto
                listUsersDto.Add(_mapper.Map<UserDto>(lista));
            }
            return Ok(listUsersDto);
        }

        [HttpGet("{userId:int}", Name = "GetUser")] // se reciben los parametros en el api
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser(int userId)
        {
            var itemUser = _usRepo.GetUser(userId);
            if (itemUser == null)
            {
                return NotFound();
            }
            var itemUserDto = _mapper.Map<UserDto>(itemUser);

            return Ok(itemUserDto);
        }

        [Authorize] //autorización por roles
        [HttpPost("registro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Registro([FromBody] UserCreateDto createUserDto)
        {
            bool validarUserName = _usRepo.IsUniqueUser(createUserDto.Cve_Usuario); //validar si el nomnbre de usuario existe
            if (!validarUserName)
            {
                _responseApi.StatusCode = HttpStatusCode.BadRequest;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add("El nombre del usuario ya existe");
                return BadRequest(_responseApi);
            }
            var user = _usRepo.Register(createUserDto);
            if (user == null)
            {
                _responseApi.StatusCode = HttpStatusCode.BadRequest;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add("El al registrar");
                return BadRequest(_responseApi);
            }
            _responseApi.StatusCode = HttpStatusCode.OK;
            _responseApi.IsSuccess = true;
            return Ok(_responseApi);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var responseLogin = await _usRepo.Login(userLoginDto);

            if (responseLogin.User == null || string.IsNullOrEmpty(responseLogin.Token))
            {
                _responseApi.StatusCode = HttpStatusCode.BadRequest;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add("El nombre del usuario o password son erroneos");
                return BadRequest(_responseApi);
            }

            _responseApi.StatusCode = HttpStatusCode.OK;
            _responseApi.IsSuccess = true;
            _responseApi.Result = responseLogin;
            return Ok(_responseApi);
        }
    }
}
