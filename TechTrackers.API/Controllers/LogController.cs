using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TechTrackers.Service;
using TechTrackers.Data.Model;
using TechTrackers.Data.Model.dto;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TechTrackers.Data;

namespace TechTrackers.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [EnableCors("corspolicy")]
    public class LogController : ControllerBase
    {
        private readonly LogService _logService;
        private readonly ILogger<LogController> _logger;
        private readonly TechTrackersDbContext? _dbContext;

        public LogController(TechTrackersDbContext dbContext, LogService logService, ILogger<LogController> logger)
        {
            _logService = logService;
            _logger = logger;
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpPost]
        public async Task<IActionResult> CreateLog([FromBody] LogDto logDto)
        {
            /*try
            {
                //int staffId = logDto.Staff_ID;  // Ensure staffId is extracted
                var log = await _logService.LogIssue(logDto);
                return Ok(log);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Key not found while creating log.");
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Invalid operation while creating log.");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}"); // Log the main error
                if (ex.InnerException != null)
                {
                    // Log the inner exception if available
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                }
                return StatusCode(500, new { message = "An internal server error occurred." });
            }*/

            if (logDto == null)
            {
                return BadRequest(new { message = "Invalid log data received." });
            }

            try
            {
                var log = await _logService.LogIssue(logDto);
                return Ok(log);
            }
            catch (ValidationException ex)
            {
                // Capture detailed validation errors if available
                return BadRequest(new { message = ex.Message, errors = ex.Data });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error details:", ex); // Detailed error logging on the server
                return StatusCode(500, new { message = "An internal server error occurred." });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogDetailDto>>> GetLogs()
        {
            try
            {
                var logs = await _logService.GetAllLogsAsync();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving logs.");
                return StatusCode(500, new { message = "An internal server error occurred." });
            }
        }

        [HttpPost("AssignTechnician")]
        public async Task<IActionResult> AssignTechnician([FromBody] AssignTechnicianDto assignDto)
        {
            try
            {
                var log = await _dbContext.Logs.FindAsync(assignDto.LogId);
                if (log == null)
                {
                    return NotFound(new { message = "Log not found." });
                }

                // Update TechnicianId in the log
                log.TechnicianId = assignDto.TechnicianId;
                _dbContext.Logs.Update(log);
                await _dbContext.SaveChangesAsync();

                return Ok(new { message = "Technician assigned successfully." });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error details:", ex); // Detailed error logging on the server
                return StatusCode(500, new { message = "An internal server error occurred." });
            }
        }

    }
}
