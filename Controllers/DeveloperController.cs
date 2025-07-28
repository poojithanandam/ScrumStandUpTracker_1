using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ScrumStandUpTracker_1.DTOs;
using ScrumStandUpTracker_1.Service;

namespace ScrumStandUpTracker_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly DeveloperService _service;
        public DeveloperController(DeveloperService service)
        {
            _service = service;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO registrationdto)
        {
            if(registrationdto==null || string.IsNullOrWhiteSpace(registrationdto.Username) || string.IsNullOrWhiteSpace(registrationdto.Password))
            {
                return BadRequest("Username and Password are required");
            }
            bool result = await _service.RegisterDeveloper(registrationdto.Username,registrationdto.Password);
            if (!result)
            {
                return BadRequest("Username already exists");
            }
            return Ok("registration successful");
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginDeveloper([FromBody] LoginDTO logindto)
        {
            if(logindto==null || string.IsNullOrWhiteSpace(logindto.Username) || string.IsNullOrWhiteSpace(logindto.Password))
            {
                return BadRequest("username and password are required");    
            }
            var developer = await _service.LoginDeveloper(logindto.Username, logindto.Password);
            if (developer == null)
            {
                return Unauthorized("Invalid Username or password");
            }
            var token = _service.GenerateJwtToken(developer);
            return Ok(new
            {
                token,
                developer.DeveloperId,
                developer.Username
            });
        }
    }
}
