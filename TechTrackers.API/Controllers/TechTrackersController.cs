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
            var respondWrapper = new RespondWrapper {
                IsSuccess = false,
                Message = "Unable to add user."
            };

            var results = await _techTrackerService.RegisterUser(user);

            if (results != null)
            {
                respondWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "User successfully registered.",
                    Result = results
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

            if(results.Count() > 0)
            {
                respondWrapper = new RespondWrapper()
                {
                    IsSuccess = true,
                    Message = "User successfully retrieved.",
                    Result = results
                };
            }

            return Ok(respondWrapper);
        }

        //Notifications
        [HttpPost]
        public async Task<IActionResult> Notify([FromBody] Notifications notifications)
        {
            var respondWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to send notification."
            };

            var results = await _techTrackerService.SendNotification(notifications);

            if (results != null)
            {
                respondWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Notification sent.",
                    Result = results
                };
            }

            return Ok(respondWrapper);
        }

        [HttpGet]
        public IActionResult RecieveNotification()
        {
            var respondWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to fetch notification."
            };

            var results = _techTrackerService.GetNotification();

            if (results.Count() > 0)
            {
                respondWrapper = new RespondWrapper()
                {
                    IsSuccess = true,
                    Message = "1 new notification recieved.",
                    Result = results
                };
            }

            return Ok(respondWrapper);
        }
    }
}
