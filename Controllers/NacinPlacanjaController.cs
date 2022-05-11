using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZdravaHranaWebShop.Entities;
using ZdravaHranaWebShop.IRepository;
using ZdravaHranaWebShop.Models;

namespace ZdravaHranaWebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NacinPlacanjaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<NacinPlacanjaController> _logger;
        private readonly IMapper _mapper;

        public NacinPlacanjaController(IUnitOfWork unitOfWork, ILogger<NacinPlacanjaController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNacinePlacanja([FromQuery] RequestParams requestParams)
        {
            try
            {
                var naciniPlacanja = await _unitOfWork.NaciniPlacanja.GetPagedList(requestParams);
                var results = _mapper.Map<IList<NacinPlacanjaDTO>>(naciniPlacanja);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(GetNacinePlacanja)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [HttpGet("{id:Guid}", Name = "GetNacinPlacanja")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNacinPlacanja(Guid id)
        {
            try
            {
                var nacinPlacanja = await _unitOfWork.NaciniPlacanja.Get(q => q.nacinID == id, new List<string> { "Porudzbine" });
                var result = _mapper.Map<NacinPlacanjaDTO>(nacinPlacanja);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(GetNacinPlacanja)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNacinPlacanja([FromBody] CreateNacinPlacanjaDTO nacinPlacanjaDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno dodavanje {nameof(CreateNacinPlacanja)}");
                return BadRequest(ModelState);
            }
            try
            {
                var nacinPlacanja = _mapper.Map<NacinPlacanja>(nacinPlacanjaDTO);
                await _unitOfWork.NaciniPlacanja.Insert(nacinPlacanja);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetNacinPlacanja", new { id = nacinPlacanja.nacinID }, nacinPlacanja);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(CreateNacinPlacanja)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateNacinPlacanja(Guid id, [FromBody] UpdateNacinPlacanjaDTO nacinPlacanjaDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno azuriranje {nameof(UpdateNacinPlacanja)}");
                return BadRequest(ModelState);
            }
            try
            {
                var nacinPlacanja = await _unitOfWork.NaciniPlacanja.Get(q => q.nacinID == id);
                if (nacinPlacanja == null)
                {
                    _logger.LogError($"Neuspesno azuriranje {nameof(UpdateNacinPlacanja)}");
                    return BadRequest("Uneti podaci su neispravni.");
                }

                _mapper.Map(nacinPlacanjaDTO, nacinPlacanja);
                _unitOfWork.NaciniPlacanja.Update(nacinPlacanja);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(UpdateNacinPlacanja)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteNacinPlacanja(Guid id)
        {
            /*if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno azuriranje {nameof(UpdateNacinPlacanja)}");
                return BadRequest(ModelState);
            }*/
            try
            {
                var nacinPlacanja = await _unitOfWork.NaciniPlacanja.Get(q => q.nacinID == id);
                if (nacinPlacanja == null)
                {
                    _logger.LogError($"Neuspesno brisanje {nameof(DeleteNacinPlacanja)}");
                    return BadRequest("Uneti podaci su neispravni.");
                }

                await _unitOfWork.NaciniPlacanja.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(DeleteNacinPlacanja)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }
    }
}
