﻿using Microsoft.AspNetCore.Mvc;
using TechTrackers.Service.Services;

namespace TechTrackers.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserLogService _userService;

        public UserController(UserLogService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto) 
        {
            try
            {
                var userWithRoles = await _userService.Login(loginDto.Email, loginDto.Password);

                if (userWithRoles.Roles.Contains("Admin"))
                {
                    return Ok(new { message = "Welcome Admin", user = userWithRoles });
                }
                else if (userWithRoles.Roles.Contains("HOD"))
                {
                    return Ok(new { message = "Welcome Head of Department", user = userWithRoles });
                }
                else if (userWithRoles.Roles.Contains("Technician"))
                {
                    return Ok(new { message = "Welcome Technician", user = userWithRoles });
                }
                else if (userWithRoles.Roles.Contains("Staff"))
                {
                    return Ok(new { message = "Welcome Staff", user = userWithRoles });
                }

                return Unauthorized(new { message = "Unauthorized role" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
      }
    public class LoginDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

}
