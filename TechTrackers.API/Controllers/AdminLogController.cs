using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TechTrackers.Data;
using TechTrackers.Data.Model.dto;
using TechTrackers.Service;

namespace TechTrackers.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [EnableCors("corspolicy")]
    public class AdminLogController : ControllerBase
    {
        private readonly AdminLogsService _adminLogsService;
        private readonly ILogger<AdminLogController> _logger;
        private readonly TechTrackersDbContext? _dbContext;

        public AdminLogController(TechTrackersDbContext dbContext, AdminLogsService adminLogsService, ILogger<AdminLogController> logger)
        {
            _adminLogsService = adminLogsService;
            _logger = logger;
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogDetailDto>>> GetLogs()
        {
            try
            {
                var logs = await _adminLogsService.DispalyAllLogsAdmin();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving logs.");
                return StatusCode(500, new { message = "An internal server error occurred.", details = ex.Message });
            }
        }
    }
}
