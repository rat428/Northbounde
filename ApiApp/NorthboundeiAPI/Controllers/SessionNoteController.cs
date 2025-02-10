using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthboundeiAPI.Models;
using NorthboundeiAPI.Services;

namespace NorthboundeiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionNoteController : ControllerBase
    {
        ISessionNoteService _sessionNoteService;
        public SessionNoteController(ISessionNoteService sessionNoteService)
        {
            _sessionNoteService = sessionNoteService;
        }

        [HttpGet("Notes")]
        //[Authorize]
        public async Task<IActionResult> Notes()
        {
            try
            {
                var result = await _sessionNoteService.GetSessionNotes();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
    }
}
