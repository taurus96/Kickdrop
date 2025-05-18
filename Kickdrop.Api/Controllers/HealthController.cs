using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Kickdrop.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Kickdrop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly KickdropContext _context;

        public HealthController(KickdropContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _context.Database.OpenConnectionAsync();
                await _context.Database.CloseConnectionAsync();
                return Ok("OK");
            }
            catch (Exception ex)
            {
                return StatusCode(503, $"Database Unavailable: {ex.Message}");
            }
        }
    }
}
