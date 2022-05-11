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
    public class AdresaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AdresaController> _logger;
        private readonly IMapper _mapper;

        public AdresaController(IUnitOfWork unitOfWork, ILogger<AdresaController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAdrese([FromQuery] RequestParams requestParams)
        {
            try
            {
                var adrese = await _unitOfWork.Adrese.GetPagedList(requestParams);
                var results = _mapper.Map<IList<AdresaDTO>>(adrese);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(GetAdrese)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [HttpGet("{id:Guid}", Name = "GetAdresa")]
        public async Task<IActionResult> GetAdresa(Guid id)
        {
            try
            {
                var adresa = await _unitOfWork.Adrese.Get(q => q.adresaID == id);
                var result = _mapper.Map<KorpaDTO>(adresa);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(GetAdresa)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAdresa([FromBody] CreateAdresaDTO adresaDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno dodavanje {nameof(CreateAdresa)}");
                return BadRequest(ModelState);
            }
            try
            {
                var adresa = _mapper.Map<Adresa>(adresaDTO);
                await _unitOfWork.Adrese.Insert(adresa);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetAdresa", new { id = adresa.adresaID }, adresa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(CreateAdresa)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAdresa(Guid id, [FromBody] UpdateAdresaDTO adresaDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno azuriranje {nameof(UpdateAdresa)}");
                return BadRequest(ModelState);
            }
            try
            {
                var adresa = await _unitOfWork.Adrese.Get(q => q.adresaID == id);
                if (adresa == null)
                {
                    _logger.LogError($"Neuspesno azuriranje {nameof(UpdateAdresa)}");
                    return BadRequest("Uneti podaci su neispravni.");
                }

                _mapper.Map(adresaDTO, adresa);
                _unitOfWork.Adrese.Update(adresa);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(UpdateAdresa)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAdresa(Guid id)
        {
            /*if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno azuriranje {nameof(UpdateAdresa)}");
                return BadRequest(ModelState);
            }*/
            try
            {
                var adresa = await _unitOfWork.Adrese.Get(q => q.adresaID == id);
                if (adresa == null)
                {
                    _logger.LogError($"Neuspesno brisanje {nameof(DeleteAdresa)}");
                    return BadRequest("Uneti podaci su neispravni.");
                }

                await _unitOfWork.Adrese.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(DeleteAdresa)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }
    }
}
