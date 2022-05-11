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
    public class StavkaKorpeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StavkaKorpeController> _logger;
        private readonly IMapper _mapper;

        public StavkaKorpeController(IUnitOfWork unitOfWork, ILogger<StavkaKorpeController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStavkeKorpe([FromQuery] RequestParams requestParams)
        {
            try
            {
                var stavkeKorpe = await _unitOfWork.StavkeKorpe.GetPagedList(requestParams);
                var results = _mapper.Map<IList<StavkaKorpeDTO>>(stavkeKorpe);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(GetStavkeKorpe)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet("{id:Guid}", Name = "GetStavkaKorpe")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStavkaKorpe(Guid id)
        {
            try
            {
                var stavkaKorpe = await _unitOfWork.StavkeKorpe.Get(q => q.stavkaKorpeID == id);
                var result = _mapper.Map<StavkaKorpeDTO>(stavkaKorpe);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(GetStavkaKorpe)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateStavkaKorpe([FromBody] CreateStavkaKorpeDTO stavkaKorpeDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno dodavanje {nameof(CreateStavkaKorpe)}");
                return BadRequest(ModelState);
            }
            try
            {
                var stavkaKorpe = _mapper.Map<StavkaKorpe>(stavkaKorpeDTO);
                await _unitOfWork.StavkeKorpe.Insert(stavkaKorpe);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetStavkaKorpe", new { id = stavkaKorpe.stavkaKorpeID }, stavkaKorpe);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(CreateStavkaKorpe)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "User")]
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateStavkaKorpe(Guid id, [FromBody] UpdateStavkaKorpeDTO stavkaKorpeDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno azuriranje {nameof(UpdateStavkaKorpe)}");
                return BadRequest(ModelState);
            }
            try
            {
                var stavkaKorpe = await _unitOfWork.StavkeKorpe.Get(q => q.stavkaKorpeID == id);
                if (stavkaKorpe == null)
                {
                    _logger.LogError($"Neuspesno azuriranje {nameof(UpdateStavkaKorpe)}");
                    return BadRequest("Uneti podaci su neispravni.");
                }

                _mapper.Map(stavkaKorpeDTO, stavkaKorpe);
                _unitOfWork.StavkeKorpe.Update(stavkaKorpe);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(UpdateStavkaKorpe)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "User")]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteStavkaKorpe(Guid id)
        {
           /*if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno azuriranje {nameof(UpdateStavkaKorpe)}");
                return BadRequest(ModelState);
            }*/
            try
            {
                var stavkaKorpe = await _unitOfWork.StavkeKorpe.Get(q => q.stavkaKorpeID == id);
                if (stavkaKorpe == null)
                {
                    _logger.LogError($"Neuspesno brisanje {nameof(DeleteStavkaKorpe)}");
                    return BadRequest("Uneti podaci su neispravni.");
                }

                await _unitOfWork.StavkeKorpe.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(DeleteStavkaKorpe)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }
    }
}
