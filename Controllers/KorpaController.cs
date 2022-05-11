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
    public class KorpaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<KorpaController> _logger;
        private readonly IMapper _mapper;

        public KorpaController(IUnitOfWork unitOfWork, ILogger<KorpaController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetKorpe([FromQuery] RequestParams requestParams)
        {
            try
            {
                var korpe = await _unitOfWork.Korpe.GetPagedList(requestParams);
                var results = _mapper.Map<IList<KorpaDTO>>(korpe);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(GetKorpe)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [HttpGet("{id:Guid}", Name = "GetKorpa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetKorpa(Guid id)
        {
            try
            {
                var korpa = await _unitOfWork.Korpe.Get(q => q.korpaID == id, new List<string> { "StavkeKorpe" });
                var result = _mapper.Map<KorpaDTO>(korpa);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(GetKorpa)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateKorpa([FromBody] KorpaDTO korpaDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno dodavanje {nameof(CreateKorpa)}");
                return BadRequest(ModelState);
            }
            try
            {
                var korpa = _mapper.Map<Korpa>(korpaDTO);
                korpa.datum = DateTime.Now;
                await _unitOfWork.Korpe.Insert(korpa);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetKorpa", new { id = korpa.korpaID }, korpa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(CreateKorpa)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }
    }
}
