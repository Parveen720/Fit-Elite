using Fit_Elite.Application.DTOs;
using Fit_Elite.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fit_Elite.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

       
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

      
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var result = await _authService.RegisterAsync(registerDto);

            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.Message }); 
            }

            return Ok(result); 
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(loginDto);

            if (!result.IsSuccess)
            {
                return Unauthorized(new { message = result.Message }); 
            }

            return Ok(result); 
        }
    }
}