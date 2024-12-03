using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TechTrackers.Service;
using Microsoft.AspNetCore.Mvc;
using TechTrackers.Data;

namespace TechTrackers.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewNotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly TechTrackersDbContext _dbContext;

        public NewNotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // Fetch notifications for a specific user
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserNotifications(int userId)
        {
            try
            {
                var notifications = await _notificationService.GetNotifications(userId);
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving notifications.", error = ex.Message });
            }
        }

        // Mark a notification as read
        [HttpPost("markAsRead/{notificationId}")]
        public async Task<IActionResult> MarkNotificationAsRead(int notificationId)
        {
            var notification = await _dbContext.Notifications.FindAsync(notificationId);
            if (notification == null)
                return NotFound(new { message = "Notification not found" });

            notification.ReadStatus = true;
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Notification marked as read" });
        }
    }
}
