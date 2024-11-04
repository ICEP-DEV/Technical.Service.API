using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TechTrackers.Data.Model.dto;
using TechTrackers.Service.AdminService;

namespace TechTrackers.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("corspolicy")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("AddSLA")]
        public async Task<IActionResult> AddSLA([FromBody] string priority)
        {
            try
            {
                var sla = await _adminService.AddSLA(priority);
                return Ok(new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "SLA added successfully.",
                    Result = sla
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new RespondWrapper
                {
                    IsSuccess = false,
                    Message = ex.Message // This will provide feedback on the duplicate priority issue
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new RespondWrapper
                {
                    IsSuccess = false,
                    Message = "An error occurred while adding the SLA."
                });
            }
        }

        [HttpPost("AssignSLAToLog")]
        public async Task<IActionResult> AssignSLAToLog(int slaId, int logId)
        {
            var result = await _adminService.AssignSLAToLog(slaId, logId);
            return Ok(new RespondWrapper
            {
                IsSuccess = result,
                Message = result ? "SLA assigned to log successfully." : "Failed to assign SLA to log."
            });
        }


        [HttpPost("CheckSLACompliance")]
        public async Task<IActionResult> CheckSLACompliance([FromBody] SLAComplianceDto complianceDto)
        {
            // Check SLA compliance and handle notifications
            var result = await _adminService.CheckAndHandleSLACompliance(complianceDto);

            if (result)
            {
                return Ok(new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "SLA breached and escalated. Notification sent to HOD."
                });
            }
            else
            {
                return Ok(new RespondWrapper
                {
                    IsSuccess = false,
                    Message = "SLA is within compliance."
                });
            }
        }
    }
}
