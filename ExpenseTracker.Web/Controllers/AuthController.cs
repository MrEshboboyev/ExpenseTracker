using ExpenseTracker.Application.Common.Models;
using ExpenseTracker.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // inject AuthService
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var token = await _authService.LoginAsync(model);
            if (token == null) return Unauthorized();
            return Ok(new { Token = token });
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var result = await _authService.RegisterAsync(model);
            if (result == null) return BadRequest("User registration failed.");
            if (!string.IsNullOrEmpty(result)) return BadRequest(result);
            return Ok(result);
        }
    }
}
