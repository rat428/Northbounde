using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NorthboundeiAPI.DTO;
using NorthboundeiAPI.Models;

namespace NorthboundeiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public StatusController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("userstatus")]
        public async Task<IActionResult> GetUserStatus(string username)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                StatusDTO usertStatus = new()
                {
                    IsOnline = user.IsOnline,
                    Username = user.UserName
                };
                return Ok(usertStatus);
            }
            catch (Exception ex)
            {
                return NotFound("User not found!");
            }
        }
    }
}
