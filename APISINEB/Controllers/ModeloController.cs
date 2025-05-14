using ApiPeliculas.Models;
using APISINEB.Models;
using APISINEB.Models.Dtos;
using APISINEB.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APISINEB.Controllers
{
    [Route("api/modelos")]
    [ApiController]
    public class ModeloController : ControllerBase
    {
        private readonly IModeloReposity _modRepo;
        private readonly IMapper _mapper;
        protected ApiResponse _responseApi; //llamamos la clase de respuestas

        public ModeloController(IModeloReposity modRepo, IMapper mapper)
        {
            _modRepo = modRepo;
            _mapper = mapper;
            this._responseApi = new(); //asi se accede a las respuestas de ResponseApi
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetModelos()
        {
            var listModel = _modRepo.GetModelos();

            var listModelDto = new List<ModeloDto>();
            foreach (var lista in listModel)
            {
                //pasar de lista modelos a modeloDTO
                listModelDto.Add(_mapper.Map<ModeloDto>(lista));
            }
            return Ok(listModelDto);
        }

        //consultar modelo por ID
        [AllowAnonymous]
        [HttpGet("{modeloId:int}", Name = "GetModelo")] // se reciben los parametros en el api
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetModelo(int modeloId)
        {
            var itemModelo = _modRepo.GetModelo(modeloId);
            if (itemModelo == null)
            {
                return NotFound();
            }
            var itemModeloDto = _mapper.Map<ModeloDto>(itemModelo);

            return Ok(itemModeloDto);
        }

        //crear una nuevo modelo por POST
        //[Authorize(Roles = "Admin")] //autorización por roles
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateModelo([FromBody] CreateModeloDto createModeloDto)
        {
            if (!ModelState.IsValid)
            { //validr si el modelo no es valido
                return BadRequest(ModelState);
            }

            if (createModeloDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_modRepo.ExistsModelo(createModeloDto.Id_Modelo))
            {
                ModelState.AddModelError("", $"El número de Modelo ya existe");
                return StatusCode(404, ModelState);
            }
            if (_modRepo.ExistsModelo(createModeloDto.Descripcion))
            {
                ModelState.AddModelError("", $"El Modelo ya existe");
                return StatusCode(404, ModelState);
            }

            var modelo = _mapper.Map<Modelos>(createModeloDto);
            if (!_modRepo.CreateModelo(modelo))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro {modelo.Id_Modelo} - {modelo.Descripcion}");
                return StatusCode(404, ModelState);
            }

            return CreatedAtRoute("GetModelo", new { modeloId = modelo.Id_Modelo }, modelo);
        }

    }
}
