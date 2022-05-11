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
    public class ProizvodController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProizvodController> _logger;
        private readonly IMapper _mapper;

        public ProizvodController(IUnitOfWork unitOfWork, ILogger<ProizvodController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProizvode([FromQuery] RequestParams requestParams)
        {
            try
            {
                var proizvodi = await _unitOfWork.Proizvodi.GetPagedList(requestParams);
                var results = _mapper.Map<IList<ProizvodDTO>>(proizvodi);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(GetProizvode)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [HttpGet("{id:Guid}", Name = "GetProizvod")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProizvod(Guid id)
        {
            try
            {
                var proizvod = await _unitOfWork.Proizvodi.Get(q => q.proizvodID == id);
                var result = _mapper.Map<ProizvodDTO>(proizvod);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(GetProizvod)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProizvod([FromBody] CreateProizvodDTO proizvodDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno dodavanje {nameof(CreateProizvod)}");
                return BadRequest(ModelState);
            }
            try
            {
                var proizvod = _mapper.Map<Proizvod>(proizvodDTO);
                await _unitOfWork.Proizvodi.Insert(proizvod);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetProizvod", new { id = proizvod.proizvodID }, proizvod);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(CreateProizvod)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProizvod(Guid id, [FromBody] UpdateProizvodDTO proizvodDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno azuriranje {nameof(UpdateProizvod)}");
                return BadRequest(ModelState);
            }
            try
            {
                var proizvod = await _unitOfWork.Proizvodi.Get(q => q.proizvodID == id);
                if (proizvod == null)
                {
                    _logger.LogError($"Neuspesno azuriranje {nameof(UpdateProizvod)}");
                    return BadRequest("Uneti podaci su neispravni.");
                }

                _mapper.Map(proizvodDTO, proizvod);
                _unitOfWork.Proizvodi.Update(proizvod);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(UpdateProizvod)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProizvod(Guid id)
        {
            /*if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno azuriranje {nameof(UpdateProizvod)}");
                return BadRequest(ModelState);
            }*/
            try
            {
                var proizvod = await _unitOfWork.Proizvodi.Get(q => q.proizvodID == id);
                if (proizvod == null)
                {
                    _logger.LogError($"Neuspesno brisanje {nameof(DeleteProizvod)}");
                    return BadRequest("Uneti podaci su neispravni.");
                }

                await _unitOfWork.Proizvodi.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(DeleteProizvod)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }
    }
}
