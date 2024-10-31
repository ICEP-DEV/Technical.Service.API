using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TechTrackers.Data.Model.dto;
using TechTrackers.Data.Model;
using TechTrackers.Data;
using Microsoft.EntityFrameworkCore;

namespace TechTrackers.API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    [EnableCors("corspolicy")]
    public class FeedbackController : ControllerBase
    {
        private readonly TechTrackersDbContext _context;

        public FeedbackController(TechTrackersDbContext context)
        {
            _context = context;
        }

        // POST: api/Feedback
        [HttpPost]
        public async Task<IActionResult> SubmitFeedback([FromBody] FeedbackDto feedbackDto)
        {
            if (feedbackDto == null || feedbackDto.Rating == 0)
                return BadRequest("Invalid feedback submission");

            var feedback = new Feedback
            {
                Log_ID = feedbackDto.Log_ID,
                User_ID = feedbackDto.User_ID,
                Rating = feedbackDto.Rating,
                Comment = feedbackDto.Comment,
                Timestamp = DateTime.Now
            };

            _context.Feed_back.Add(feedback);
            await _context.SaveChangesAsync();

            return Ok("Feedback submitted successfully");
        }

        // GET: api/Feedback/{logId}
        [HttpGet("{logId}")]
        public async Task<IActionResult> GetFeedbackByLog(int logId)
        {
            var feedbackList = await _context.Feed_back
                .Where(f => f.Log_ID == logId)
                .ToListAsync();

            if (!feedbackList.Any())
                return NotFound("No feedback found for the given Log ID");

            return Ok(feedbackList);
        }
    }
}
