using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TechTrackers.Data.Model.dto;
using TechTrackers.Data.Model;
using TechTrackers.Service.Authorization;

namespace TechTrackers.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("corspolicy")]
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] User user)
        {
            var result = await _authService.SignUp(user);

            if (result != null)
            {
                return Ok(new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "User registered successfully",
                    Result = result
                });
            }

            return BadRequest(new RespondWrapper
            {
                IsSuccess = false,
                Message = "User registration failed"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User loginData)
        {
            var result = await _authService.Login(loginData.Email, loginData.Password);

            if (result != null)
            {
                return Ok(new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Login successful",
                    Result = result
                });
            }

            return Unauthorized(new RespondWrapper
            {
                IsSuccess = false,
                Message = "Invalid email or password"
            });
        }

    }
}
