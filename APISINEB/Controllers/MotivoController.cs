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
    [Route("api/motivos")]
    [ApiController]
    public class MotivoController : ControllerBase
    {
        private readonly IMotivoRepository _motRepo;
        private readonly IMapper _mapper;
        protected ApiResponse _responseApi;

        public MotivoController(IMotivoRepository motRepo, IMapper mapper)
        {
            _motRepo = motRepo;
            _mapper = mapper;
            this._responseApi = new();
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMotivos()
        {
            var listMotive = _motRepo.GetMotivos();

            var listMotiveDto = new List<MotivoDto>();
            foreach (var lista in listMotive)
            {
                listMotiveDto.Add(_mapper.Map<MotivoDto>(lista));
            }
            return Ok(listMotiveDto);
        }

        //consulta por Id
        [AllowAnonymous]
        [HttpGet("id/{motivoId:int}", Name = "GetMotivo")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMotivo(int motivoId)
        {
            var itemMotivo = _motRepo.GetMotivo(motivoId);
            if (itemMotivo == null)
            {
                return NotFound();
            }
            var itemMotivoDto = _mapper.Map<MotivoDto>(itemMotivo);

            return Ok(itemMotivoDto);
        }

        //consulta por Cve
        [AllowAnonymous]
        [HttpGet("cve/{motivoCve:int}", Name = "GetMotivoCve")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMotivoCve(int motivoCve)
        {
            var itemMotivo = _motRepo.GetMotivoCve(motivoCve);
            if (itemMotivo == null)
            {
                return NotFound();
            }
            var itemMotivoDto = _mapper.Map<MotivoDto>(itemMotivo);

            return Ok(itemMotivoDto);
        }

        //crear motivo
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateMotivo([FromBody] CreateMotivoDto createMotivoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (createMotivoDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_motRepo.ExistsMotivoCve(createMotivoDto.Cve_Motivo))
            {
                ModelState.AddModelError("", $"El número de motivo ya existe");
                return StatusCode(404, ModelState);
            }

            if (_motRepo.ExistsMotivo(createMotivoDto.Descripcion))
            {
                ModelState.AddModelError("", $"El motivo ya existe");
                return StatusCode(404, ModelState);
            }

            var motivo = _mapper.Map<Motivos>(createMotivoDto);
            if (!_motRepo.CreateMotivo(motivo))
            {
                ModelState.AddModelError("",$"Algo salió mal guardando el registro {motivo.Cve_Motivo} - {motivo.Descripcion}");
                return StatusCode(404, ModelState);
            }

            return CreatedAtRoute("GetMotivoCve", new { motivoCve = motivo.Cve_Motivo }, motivo);
        }
    }
}
