using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZdravaHranaWebShop.Entities;
using ZdravaHranaWebShop.IRepository;
using ZdravaHranaWebShop.Models;
using ZdravaHranaWebShop.Services;

namespace ZdravaHranaWebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(UserManager<User> userManager, ILogger<AccountController> logger, IMapper mapper, IAuthManager authManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation($"Registration attempt for {userDTO.Email}");
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = _mapper.Map<User>(userDTO);
                user.UserName = userDTO.Email;
                var result = await _userManager.CreateAsync(user, userDTO.Password);

                if(!result.Succeeded)
                {
                    return BadRequest($"Neuspesna registracija.");
                }

                await _userManager.AddToRolesAsync(user, userDTO.Roles);
                return Accepted();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(Register)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            _logger.LogInformation($"Login attempt for {userDTO.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                if (!await _authManager.ValidateUser(userDTO))
                {
                    return Unauthorized();
                }

                return Accepted(new { Token = await _authManager.CreateToken() });
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(Login)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "User")]
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser(string email, [FromBody] UpdateUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno azuriranje {nameof(UpdateUser)}");
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _unitOfWork.Users.Get(q => q.Email == email);
                if (user == null)
                {
                    _logger.LogError($"Neuspesno azuriranje {nameof(UpdateUser)}");
                    return BadRequest("Uneti podaci su neispravni.");
                }

                _mapper.Map(userDTO, user);
                _unitOfWork.Users.Update(user);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(UpdateUser)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _unitOfWork.Users.GetAll();
                var results = _mapper.Map<IList<UserDTO>>(users);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(GetUsers)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser(string email)
        {
            try
            {
                var user = await _unitOfWork.Users.Get(q => q.Email == email);
                var result = _mapper.Map<UserDTO>(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(GetUser)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }
        /*
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser(string email)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Neuspesno azuriranje {nameof(UpdateTipProizvoda)}");
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _unitOfWork.Users.Get(q => q.Email == email);
                if (user == null)
                {
                    _logger.LogError($"Neuspesno brisanje {nameof(DeleteUser)}");
                    return BadRequest("Uneti podaci su neispravni.");
                }

                await _unitOfWork.Users.Delete(email);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Doslo je do greske prilikom {nameof(DeleteUser)}");
                return StatusCode(500, "Pokusajte opet kasnije.");
            }
        }*/
    }
}
