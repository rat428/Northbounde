using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NorthboundeiAPI.DTO;
using NorthboundeiAPI.IServices;
using NorthboundeiAPI.Models;
using NorthboundeiAPI.Services;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NorthboundeiAPI.Controllers;


/// <summary>
/// Authentication controller to handle user registration, login, and other authentication-related actions.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IAuthService _authService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
    /// </summary>
    /// <param name="userManager">The user manager.</param>
    /// <param name="jwtTokenGenerator">The JWT token generator.</param>
    /// <param name="authService">The authentication service.</param>
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

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="registerDto">The registration data transfer object.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var user = new ApplicationUser
        {
            UserName = registerDto.Username,
            Email = registerDto.Email,
            ExpirationDate = DateTime.UtcNow.AddDays(1),
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

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="loginDto">The login data transfer object.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            return Unauthorized("Wrong Password & Username combination");
        }
        var expiration = user.PasswordExpirationDate - DateTime.UtcNow;
        if (expiration.Days > 0)
        {
            return Ok(new
            {
                Token = _jwtGenerator.GenerateToken(user),
                Username = user.UserName,
                ExpirationDate = user.PasswordExpirationDate,
                // First 6 characters of the MD5 hash of the user's ID
                Key = Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(user.Id)).Take(6).ToArray()),
                Warning = $"Your password will expire in less than {expiration.Days} days."
            });
        }

        return Unauthorized("Password expired");
    }

    /// <summary>
    /// Authenticates the user.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
    [HttpGet("auth")]
    [Authorize]
    public async Task<IActionResult> Auth()
    {
        var user = await _userManager.GetUserAsync(User);

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

    /// <summary>
    /// Changes the user's password.
    /// </summary>
    /// <param name="changePasswordDto">The change password data transfer object.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var user = await _userManager.FindByNameAsync(changePasswordDto.Username);
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

    /// <summary>
    /// Generates a secure key.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
    [HttpPost("generate-key")]
    public async Task<IActionResult> GenerateKey()
    {
        var key = _jwtGenerator.GenerateSecureKey();
        return Ok(key);
    }

    /// <summary>
    /// Authenticates the service.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
    [HttpGet("service-auth")]
    //[Authorize]
    public async Task<IActionResult> ServiceAuth()
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
