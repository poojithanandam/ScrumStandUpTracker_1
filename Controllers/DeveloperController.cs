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
        private readonly ILogger<DeveloperController> _logger;
        public DeveloperController(DeveloperService service,ILogger<DeveloperController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO registrationdto)
        {
            if(registrationdto==null || string.IsNullOrWhiteSpace(registrationdto.Username) || string.IsNullOrWhiteSpace(registrationdto.Password))
            {
                _logger.LogInformation("Registration failed due to missing username or password");
                return BadRequest("Username and Password are required");
            }
            bool result = await _service.RegisterDeveloper(registrationdto.Username,registrationdto.Password);
            if (!result)
            {
                _logger.LogWarning("Registration attempt failed: Username already exists");
                return BadRequest("Username already exists");
            }
            _logger.LogInformation("Developer registered successfully");
            return Ok("registration successful");
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginDeveloper([FromBody] LoginDTO logindto)
        {
            if(logindto==null || string.IsNullOrWhiteSpace(logindto.Username) || string.IsNullOrWhiteSpace(logindto.Password))
            {
                _logger.LogWarning("Login failed due to missing username or password");
                return BadRequest("username and password are required");    
            }
            var developer = await _service.LoginDeveloper(logindto.Username, logindto.Password);
            if (developer == null)
            {
                _logger.LogWarning("login failed for username invalid credentials");
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
