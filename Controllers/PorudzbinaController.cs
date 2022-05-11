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
    public class PorudzbinaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PorudzbinaController> _logger;
        private readonly IMapper _mapper;

        public PorudzbinaController(IUnitOfWork unitOfWork, ILogger<PorudzbinaController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPorudzbine([FromQuery] RequestParams requestParams)
        {
            try
            {
                var porudzbine = await _unitOfWork.Porudzbine.GetPagedList(requestParams);
                var results = _mapper.Map<IList<PorudzbinaDTO>>(porudzbine);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(GetPorudzbine)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [HttpGet("{id:Guid}", Name = "GetPorudzbina")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPorudzbina(Guid id)
        {
            try
            {
                var porudzbina = await _unitOfWork.Porudzbine.Get(q => q.porudzbinaID == id);
                var result = _mapper.Map<PorudzbinaDTO>(porudzbina);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(GetPorudzbina)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePorudzbina([FromBody] CreatePorudzbinaDTO porudzbinaDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno dodavanje {nameof(CreatePorudzbina)}");
                return BadRequest(ModelState);
            }
            try
            {
                var porudzbina = _mapper.Map<Porudzbina>(porudzbinaDTO);
                await _unitOfWork.Porudzbine.Insert(porudzbina);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetPorudzbina", new { id = porudzbina.porudzbinaID }, porudzbina);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(CreatePorudzbina)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePorudzbina(Guid id, [FromBody] UpdatePorudzbinaDTO porudzbinaDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno azuriranje {nameof(UpdatePorudzbina)}");
                return BadRequest(ModelState);
            }
            try
            {
                var porudzbina = await _unitOfWork.Porudzbine.Get(q => q.porudzbinaID == id);
                if (porudzbina == null)
                {
                    _logger.LogError($"Neuspesno azuriranje {nameof(UpdatePorudzbina)}");
                    return BadRequest("Uneti podaci su neispravni.");
                }

                _mapper.Map(porudzbinaDTO, porudzbina);
                _unitOfWork.Porudzbine.Update(porudzbina);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(UpdatePorudzbina)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePorudzbina(Guid id)
        {
           /* if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno brisanje {nameof(DeletePorudzbina)}");
                return BadRequest(ModelState);
            }*/
            try
            {
                var porudzbina = await _unitOfWork.Porudzbine.Get(q => q.porudzbinaID == id);
                if (porudzbina == null)
                {
                    _logger.LogError($"Neuspesno brisanje {nameof(DeletePorudzbina)}");
                    return BadRequest("Uneti podaci su neispravni.");
                }

                await _unitOfWork.Porudzbine.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(DeletePorudzbina)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }
    }
}
