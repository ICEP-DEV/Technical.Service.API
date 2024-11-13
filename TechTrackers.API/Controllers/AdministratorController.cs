using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TechTrackers.Data.Model.dto;
using TechTrackers.Service.Administrator;

namespace TechTrackers.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("corspolicy")]
    public class AdministratorController : Controller
    {
        private readonly IAdministratorService _administratorService;

        public AdministratorController(IAdministratorService adminService)
        {
            _administratorService = adminService;
        }


        [HttpPost("AddSLA/{priority}")]
        public async Task<IActionResult> AddSLA(string priority)
        {
            var sla = await _administratorService.AddSLA(priority);

            if (sla != null)
            {
                return Ok(new RespondWrapper
                {
                    IsSuccess = true,
                    Message = $"SLA for {priority} priority created successfully.",
                    Result = sla
                });
            }

            // If SLA already exists or invalid priority
            return BadRequest(new RespondWrapper
            {
                IsSuccess = false,
                Message = "Failed to create SLA. Invalid priority level or SLA already exists."
            });

          /*  var response = await _administratorService.AddSLA(priority);

            return Ok(response);*/
        }

        [HttpPost("AssignSLAToLog")]
        public async Task<IActionResult> AssignSLAToLog(int slaId, int logId)
        {
            if (slaId <= 0 || logId <= 0)
            {
                return BadRequest(new RespondWrapper
                {
                    IsSuccess = false,
                    Message = "Invalid SLA ID or Log ID."
                });
            }

            // Call AssignSLAToLog in the service layer
            var response = await _administratorService.AssignSLAToLog(slaId, logId);

            // Check the response from the service and return it
            if (!response.IsSuccess)
            {
                return BadRequest(response); // Return error response directly from the service
            }

            return Ok(response); // Successful response if SLA was assigned
        }

        [HttpPost("CheckSLACompliance/{logId}")]
        public async Task<IActionResult> CheckSLACompliance(int logId)
        {
            if (logId <= 0)
            {
                return BadRequest(new RespondWrapper
                {
                    IsSuccess = false,
                    Message = "Invalid Log ID."
                });
            }

            // Call the enhanced CheckSLACompliance method in the service layer
            var response = await _administratorService.CheckAndHandleSLACompliance(logId);

            // Return detailed response from service
            return Ok(response);
        }


    }
}
