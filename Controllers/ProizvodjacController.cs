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
    public class ProizvodjacController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProizvodjacController> _logger;
        private readonly IMapper _mapper;

        public ProizvodjacController(IUnitOfWork unitOfWork, ILogger<ProizvodjacController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProizvodjace([FromQuery] RequestParams requestParams)
        {
            try
            {
                var proizvodjaci = await _unitOfWork.Proizvodjaci.GetPagedList(requestParams);
                var results = _mapper.Map<IList<ProizvodjacDTO>>(proizvodjaci);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(GetProizvodjace)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [HttpGet("{id:Guid}", Name = "GetProizvodjac")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProizvodjac(Guid id)
        {
            try
            {
                var proizvodjac = await _unitOfWork.Proizvodjaci.Get(q => q.proizvodjacID == id, new List<string> { "Proizvodi" });
                var result = _mapper.Map<ProizvodjacDTO>(proizvodjac);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(GetProizvodjac)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProizvodjac([FromBody] CreateProizvodjacDTO proizvodjacDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno dodavanje {nameof(CreateProizvodjac)}");
                return BadRequest(ModelState);
            }
            try
            {
                var proizvodjac = _mapper.Map<Proizvodjac>(proizvodjacDTO);
                await _unitOfWork.Proizvodjaci.Insert(proizvodjac);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetProizvodjac", new { id = proizvodjac.proizvodjacID }, proizvodjac);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(CreateProizvodjac)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProizvodjac(Guid id, [FromBody] UpdateProizvodjacDTO proizvodjacDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno azuriranje {nameof(UpdateProizvodjac)}");
                return BadRequest(ModelState);
            }
            try
            {
                var proizvodjac = await _unitOfWork.Proizvodjaci.Get(q => q.proizvodjacID == id);
                if (proizvodjac == null)
                {
                    _logger.LogError($"Neuspesno azuriranje {nameof(UpdateProizvodjac)}");
                    return BadRequest("Uneti podaci su neispravni.");
                }

                _mapper.Map(proizvodjacDTO, proizvodjac);
                _unitOfWork.Proizvodjaci.Update(proizvodjac);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(UpdateProizvodjac)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProizvodjac(Guid id)
        {
            /*if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno azuriranje {nameof(UpdateProizvodjac)}");
                return BadRequest(ModelState);
            }*/
            try
            {
                var proizvodjac = await _unitOfWork.Proizvodjaci.Get(q => q.proizvodjacID == id);
                if (proizvodjac == null)
                {
                    _logger.LogError($"Neuspesno brisanje {nameof(DeleteProizvodjac)}");
                    return BadRequest("Uneti podaci su neispravni.");
                }

                await _unitOfWork.Proizvodjaci.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(DeleteProizvodjac)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }
    }
}
