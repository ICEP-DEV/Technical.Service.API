using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TechTrackers.Data.Model;
using TechTrackers.Data.Model.dto;
using TechTrackers.Service;

namespace TechTrackers.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("corspolicy")]
    public class TechTrackersController : Controller
    {
        private readonly ITechTrackerService _techTrackerService;

        public TechTrackersController(ITechTrackerService techTrackerService)
        {
            _techTrackerService = techTrackerService;
        }

        //User
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            var respondWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to add user."
            };

            var result = await _techTrackerService.RegisterUser(user);

            if (result != null)
            {
                respondWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "User successfully registered.",
                    Result = result
                };
            }

            return Ok(respondWrapper);
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            var respondWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to fetch user."
            };

            var results = _techTrackerService.GetUsers();

            if (results.Any())
            {
                respondWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "User successfully retrieved.",
                    Result = results
                };
            }

            return Ok(respondWrapper);
        }
    }
}
