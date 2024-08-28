using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tabylr.Client.Models.Requests;
using Tabylr.Services.Interfaces;

namespace Tabylr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var result = await _authService.LoginAsync(request.Email, request.Password);
                if (result.Success)
                {
                    return Ok(new { Token = result.Token, User = result.User });
                }
                return BadRequest(new { Message = "Login failed", Error = result.ErrorMessage });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Login failed", Error = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var result = await _authService.RegisterAsync(request.Email, request.Password);
                if (result.Success)
                {
                    return Ok(new { Message = "Registration successful", User = result.User });
                }
                return BadRequest(new { Message = "Registration failed", Error = result.ErrorMessage });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Registration failed", Error = ex.Message });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var result = await _authService.LogoutAsync();
                if (result.Success)
                {
                    return Ok(new { Message = "Logout successful" });
                }
                return BadRequest(new { Message = "Logout failed", Error = result.ErrorMessage });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Logout failed", Error = ex.Message });
            }
        }

       
    }
}
