using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using TechTrackers.Service;
using TechTrackers.Data.Model;
using TechTrackers.Data.Model.dto;

namespace TechTrackers.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [EnableCors("corspolicy")]
    public class LogController : ControllerBase
    {
        private readonly LogService _logService;
        private readonly ILogger<LogController> _logger;

        public LogController(LogService logService, ILogger<LogController> logger)
        {
            _logService = logService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLog([FromBody] LogDto logDto)
        {
            try
            {
                int staffId = logDto.Staff_ID;  // Ensure staffId is extracted
                var log = await _logService.LogIssue(staffId, logDto);
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
=======
using TechTrackers.Data.Model;
using TechTrackers.Data.Model.dto;
using TechTrackers.Service.IssueLog;

namespace TechTrackers.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("corspolicy")]
    public class LogController : Controller
    {

        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        // POST api/LogController/LogIssue
        [HttpPost]
        public async Task<IActionResult> LogIssue([FromBody] Log log)
        {
            if (log == null)
            {
                return BadRequest(new RespondWrapper
                {
                    IsSuccess = false,
                    Message = "Log object cannot be null."
                });
            }

            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to log issue"
            };

            var result = await _logService.LogIssue(log);

            if (result != null)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Issue logged successfully",
                    Result = result
                };
            }

            return Ok(responseWrapper);

        }

        // GET api/LogController/GetAllLogs
        [HttpGet]
        public IActionResult GetAllLogs()
        {
            var respondWrapper = new RespondWrapper
            {

                IsSuccess = false,
                Message = "Failed to retrive users"
            };

            var results = _logService.GetAllLogs();
            if (results.Count() > 0)
            {
                respondWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Successfully retrieved all logs",
                    Result = results

                };
            }

            return Ok(respondWrapper); ;
        }

        // GET api/LogController/GetLogById/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLogById(int id)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Log not found"
            };

            var result = await _logService.GetLogById(id);

            if (result != null)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Successfully retrieved log",
                    Result = result
                };
            }

            return Ok(responseWrapper);
        }

        // PUT api/LogController/UpdateLog
        [HttpPut]
        public async Task<IActionResult> UpdateLog([FromBody] Log log)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to update log"
            };

            var updatedLog = await _logService.UpdateLog(log);

            if (updatedLog != null)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Log updated successfully",
                    Result = updatedLog
                };
            }

            return Ok(responseWrapper);
        }


        // DELETE api/LogController/DeleteLog/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLog(int id)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to delete log"
            };

            var isDeleted = await _logService.DeleteLog(id);

            if (isDeleted)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Log deleted successfully"
                };
            }

            return Ok(responseWrapper);
        }

        [HttpGet("GetLogsByStatus/{status}")]
        public IActionResult GetLogsByStatus(string status)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Failed to retrieve logs"
            };

            var results = _logService.GetLogsByStatus(status);
            if (results.Any())
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Successfully retrieved logs",
                    Result = results
                };
            }

            return Ok(responseWrapper);
        }

        // GET api/LogController/GetLogsByDateRange?startDate={startDate}&endDate={endDate}
        [HttpGet("GetLogsByDateRange")]
        public IActionResult GetLogsByDateRange(DateTime startDate, DateTime endDate)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Failed to retrieve logs"
            };

            var results = _logService.GetLogsByDateRange(startDate, endDate);
            if (results.Any())
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Successfully retrieved logs",
                    Result = results
                };
            }

            return Ok(responseWrapper);
        }

        // GET api/LogController/GetLogCount
        [HttpGet("GetLogCount")]
        public IActionResult GetLogCount()
        {
            var count = _logService.GetLogCount();
            return Ok(new RespondWrapper
            {
                IsSuccess = true,
                Message = "Successfully retrieved log count",
                Result = count
            });
        }

        [HttpPost("AssignLogToTechnician")]
        public async Task<IActionResult> AssignLogToTechnician([FromBody] LogAssignmentDTO logAssignmentDto)
        {
            if (logAssignmentDto == null)
            {
                return BadRequest(new RespondWrapper
                {
                    IsSuccess = false,
                    Message = "Log assignment data cannot be null."
                });
            }

            if (logAssignmentDto.Log_ID <= 0 || logAssignmentDto.Technician_ID <= 0)
            {
                return BadRequest(new RespondWrapper
                {
                    IsSuccess = false,
                    Message = "Invalid Log ID or Technician ID."
                });
            }

            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to assign log"
            };

            // Use the service layer to assign the technician
            var isAssigned = await _logService.AssignLogToTechnician(logAssignmentDto.Log_ID, logAssignmentDto.Technician_ID);

            if (isAssigned)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Log assigned successfully"
                };
            }

            return Ok(responseWrapper);
        }



        // GET api/LogController/SearchLogs?keyword={keyword}
        [HttpGet("SearchLogs")]
        public IActionResult SearchLogs(string keyword)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Failed to retrieve logs"
            };

            var results = _logService.SearchLogs(keyword);
            if (results.Any())
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Successfully retrieved logs",
                    Result = results
                };
            }

            return Ok(responseWrapper);
        }



        // POST api/LogController/ArchiveOldLogs

       /* [HttpPost("ArchiveOldLogs")]
        public async Task<IActionResult> ArchiveOldLogs(int days)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to archive logs"
            };

            var archivedCount = await _logService.ArchiveOldLogs(days);

            if (archivedCount > 0)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = $"{archivedCount} logs archived successfully"
                };
            }

            return Ok(responseWrapper);
        }*/








>>>>>>> fba63d8d4b34c85b9368550f59bb012d92a94355
    }
}
