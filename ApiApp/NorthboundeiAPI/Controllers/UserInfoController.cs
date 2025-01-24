using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthboundeiAPI.Services;

namespace NorthboundeiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        IUserService _userInfoService;
        public UserInfoController(IUserService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        [HttpGet("Users")]
        //[Authorize]
        public async Task<IActionResult> Users()
        {
            try
            {
                var result = await _userInfoService.GetUsersInfo();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
    }
}
