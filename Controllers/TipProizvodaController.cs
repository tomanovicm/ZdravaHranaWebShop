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
    public class TipProizvodaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TipProizvoda> _logger;
        private readonly IMapper _mapper;

        public TipProizvodaController(IUnitOfWork unitOfWork, ILogger<TipProizvoda> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTipoveProizvoda([FromQuery] RequestParams requestParams)
        {
            try 
            {
                var tipoviProizvoda = await _unitOfWork.TipoviProizvoda.GetPagedList(requestParams);
                var results = _mapper.Map<IList<TipProizvodaDTO>>(tipoviProizvoda);
                return Ok(results);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(GetTipoveProizvoda)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [HttpGet("{id:Guid}", Name = "GetTipProizvoda")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTipProizvoda(Guid id)
        {
            try
            {
                var tipProizvoda = await _unitOfWork.TipoviProizvoda.Get(q => q.tipProizID == id, new List<string> { "Proizvodi" });
                var result = _mapper.Map<TipProizvodaDTO>(tipProizvoda);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(GetTipProizvoda)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTipProizvoda([FromBody] CreateTipProizvodaDTO tipProizvodaDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno dodavanje {nameof(CreateTipProizvoda)}");
                return BadRequest(ModelState);
            }
            try
            {
                var tipProizvoda = _mapper.Map<TipProizvoda>(tipProizvodaDTO);
                await _unitOfWork.TipoviProizvoda.Insert(tipProizvoda);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetTipProizvoda", new { id = tipProizvoda.tipProizID }, tipProizvoda);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(CreateTipProizvoda)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTipProizvoda(Guid id, [FromBody] UpdateTipProizvodaDTO tipProizvodaDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno azuriranje {nameof(UpdateTipProizvoda)}");
                return BadRequest(ModelState);
            }
            try
            {
                var tipProizvoda = await _unitOfWork.TipoviProizvoda.Get(q => q.tipProizID == id);
                if(tipProizvoda == null)
                {
                    _logger.LogError($"Neuspesno azuriranje {nameof(UpdateTipProizvoda)}");
                    return BadRequest("Uneti podaci su neispravni.");
                }

                _mapper.Map(tipProizvodaDTO, tipProizvoda);
                _unitOfWork.TipoviProizvoda.Update(tipProizvoda);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(UpdateTipProizvoda)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTipProizvoda(Guid id)
        {
            /*if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno azuriranje {nameof(UpdateTipProizvoda)}");
                return BadRequest(ModelState);
            }*/
            try
            {
                var tipProizvoda = await _unitOfWork.TipoviProizvoda.Get(q => q.tipProizID == id);
                if (tipProizvoda == null)
                {
                    _logger.LogError($"Neuspesno brisanje {nameof(DeleteTipProizvoda)}");
                    return BadRequest("Uneti podaci su neispravni.");
                }

                await _unitOfWork.TipoviProizvoda.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(DeleteTipProizvoda)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }
    }
}
