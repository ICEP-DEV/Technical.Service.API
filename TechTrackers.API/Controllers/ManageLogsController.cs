using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TechTrackers.Data.Model.dto;
using TechTrackers.Service.ManageLogs;

namespace TechTrackers.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("corspolicy")]
    public class ManageLogsController : Controller
    {
        private readonly IManageLogs _manageLogs;

        public ManageLogsController(IManageLogs manageLogs)
        {
            _manageLogs = manageLogs;
        }

        // Count all logs
        [HttpGet]
        public async Task<IActionResult> CountAllLogs()
        {
            var count = await _manageLogs.CountAllLogsAsync();
            return Ok(new RespondWrapper
            {
                IsSuccess = true,
                Message = "Counted all logs successfully.",
                Result = count
            });
        }

        // Count logs by status
        [HttpGet("{status}")]
        public async Task<IActionResult> CountLogsByStatus(string status)
        {
            var count = await _manageLogs.CountLogsByStatusAsync(status);
            return Ok(new RespondWrapper
            {
                IsSuccess = true,
                Message = $"Counted logs with status '{status}' successfully.",
                Result = count
            });
        }

        // Get open logs
        [HttpGet]
        public async Task<IActionResult> GetOpenLogs()
        {
            var logs = await _manageLogs.GetOpenLogsAsync();
            return Ok(new RespondWrapper
            {
                IsSuccess = true,
                Message = "Retrieved open logs successfully.",
                Result = logs
            });
        }

        // Open a log
        [HttpPut("{logId}")]
        public async Task<IActionResult> OpenLog(int logId)
        {
            var success = await _manageLogs.OpenLog(logId);
            if (!success)
            {
                return NotFound(new RespondWrapper
                {
                    IsSuccess = false,
                    Message = $"Log with ID {logId} not found."
                });
            }

            return Ok(new RespondWrapper
            {
                IsSuccess = true,
                Message = $"Log with ID {logId} opened successfully."
            });
        }

        // Close a log
        [HttpPut("{logId}")]
        public async Task<IActionResult> CloseLog(int logId)
        {
            var success = await _manageLogs.CloseLog(logId);
            if (!success)
            {
                return NotFound(new RespondWrapper
                {
                    IsSuccess = false,
                    Message = $"Log with ID {logId} not found."
                });
            }

            return Ok(new RespondWrapper
            {
                IsSuccess = true,
                Message = $"Log with ID {logId} closed successfully."
            });
        }

        [HttpPut("{logId}/{newStatus}")]
        public async Task<IActionResult> ChangeLogStatus(int logId, string newStatus)
        {
            var success = await _manageLogs.ChangeLogStatus(logId, newStatus);
            if (!success)
            {
                return NotFound(new RespondWrapper
                {
                    IsSuccess = false,
                    Message = $"Log with ID {logId} not found."
                });
            }

            return Ok(new RespondWrapper
            {
                IsSuccess = true,
                Message = $"Log with ID {logId} status changed to '{newStatus}' successfully."
            });
        }

    }
}
