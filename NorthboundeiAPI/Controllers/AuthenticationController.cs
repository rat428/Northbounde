using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NorthboundeiAPI.DTO;
using NorthboundeiAPI.IServices;
using NorthboundeiAPI.Models;
using NorthboundeiAPI.Services;
using System.Security.Claims;

namespace NorthboundeiAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IAuthService _authService;


        public AuthenticationController(
            UserManager<ApplicationUser> userManager,
            IJwtGenerator jwtTokenGenerator,
            IAuthService authService
            )
        {
            _userManager = userManager;
            _jwtGenerator = jwtTokenGenerator;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                ExpirationDate= DateTime.UtcNow.AddDays(1),
                PasswordExpirationDate = DateTime.UtcNow.AddDays(5),
                TimeZone = registerDto.TimeZone
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "User registered successfully!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return Unauthorized("Wrong Password & Username combination ");
            }

            var expiration = user.PasswordExpirationDate - DateTime.UtcNow;
            if (expiration.Days > 0)
            {
                return Ok(new
                {
                    Token = _jwtGenerator.GenerateToken(user),
                    Username = user.UserName,
                    ExpirationDate = user.PasswordExpirationDate,
                    Warning = $"Your password will expire in less than {expiration.Days} days."
                });
            }

            return Unauthorized("Password expired");
        }

        [HttpGet("auth")]
        public async Task<IActionResult> Auth()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByNameAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }
            var expiration = user.PasswordExpirationDate - DateTime.UtcNow;
            if (expiration.Days > 0)
            {
                return Ok(new
                {
                    Token = _jwtGenerator.GenerateToken(user),
                    Username = user.UserName,
                    ExpirationDate = user.PasswordExpirationDate,
                    Warning = $"Your password will expire in less than {expiration.Days} days."
                });
            }
            return Unauthorized("Password expired");
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.FindByIdAsync(changePasswordDto.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);

            if (result.Succeeded)
            {
                user.PasswordExpirationDate = DateTime.UtcNow.AddDays(5);
                await _userManager.UpdateAsync(user);
                return Ok(new { Message = "Password changed successfully!" });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("generate-key")]
        public async Task<IActionResult> GenerateKey()
        {
            var key = _jwtGenerator.GenerateSecureKey();
            return Ok(key);
        }


        [HttpGet("service-auth")]
        public async Task<IActionResult> ServieAuth()
        {
            try
            {
                
                var result = await _authService.GetAllServices();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }

}
