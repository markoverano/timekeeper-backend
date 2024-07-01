using Microsoft.AspNetCore.Mvc;
using TimeKeeper.Core.DTO;
using TimeKeeper.Core.Interface.Services;

namespace TimeKeeper.Controllers
{
    [Route("api/auth")]
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
            var response = await _authService.Authenticate(request?.Email, request?.Password);
            if (response == null) return Unauthorized();

            return Ok(new LoginResponse
            {
                Token = response.Token,
                UserId = response.UserId,
                EmployeeId = response.EmployeeId,
                Permissions = response.Permissions,
                UserRole = response.UserRole
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
