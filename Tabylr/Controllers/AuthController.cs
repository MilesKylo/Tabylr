using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tabylr.Client.Models.Requests;
using Tabylr.Client.Models.Responses;
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
                    return Ok(new AuthResult { Token = result.Token, User = result.User ,Success = true });
                }
                return BadRequest(new AuthResult  { Message = "Login Failed", ErrorMessage = result.ErrorMessage , Success = false});
            }
            catch (Exception ex)
            {
                return BadRequest(new AuthResult{ Message= "Login Failed", ErrorMessage = ex.Message , Success = false });
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
                    return Ok(new AuthResult { Message = "Registration successful", User = result.User ,Success = true });
                }
                return BadRequest(new AuthResult { Message = "Registration failed", ErrorMessage = result.ErrorMessage, Success=false });
            }
            catch (Exception ex)
            {
                return BadRequest(new AuthResult { Message = "Registration failed", ErrorMessage = ex.Message , Success = false});
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
                    return Ok(new AuthResult { Message = "Logout successful" ,Success = true});
                }
                return BadRequest(new AuthResult { Message = "Logout failed", ErrorMessage = result.ErrorMessage ,Success=false});
            }
            catch (Exception ex)
            {
                return BadRequest(new AuthResult { Message = "Logout failed", ErrorMessage = ex.Message, Success = false});
            }
        }

       
    }
}
