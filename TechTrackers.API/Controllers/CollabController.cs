using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTrackers.Data;
using TechTrackers.Data.Model;
using TechTrackers.Data.Model.dto;

namespace TechTrackers.API.Controllers
{
    [ApiController]
    [Route("api/Collaboration")]
    public class CollabController : ControllerBase
    {
        private readonly TechTrackersDbContext _dbContext;

        public CollabController(TechTrackersDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); // Ensure dbContext is not null
        }

        [HttpPost("Request")]
        public async Task<IActionResult> RequestCollaboration([FromBody] CollabRequestDto requestDto)
        {
            Console.WriteLine("RequestCollaboration called.");

            // Check if requestDto is null
            if (requestDto == null)
            {
                Console.WriteLine("Error: Request data is null.");
                return BadRequest("Request data is null.");
            }

            // Validate input properties
            if (requestDto.LogId <= 0 || requestDto.RequestingTechnicianId <= 0 || requestDto.InvitedTechnicianId <= 0)
            {
                Console.WriteLine($"Invalid data: LogId={requestDto.LogId}, RequestingTechnicianId={requestDto.RequestingTechnicianId}, InvitedTechnicianId={requestDto.InvitedTechnicianId}");
                return BadRequest("Invalid data provided in the request.");
            }

            try
            {
                // Log object creation
                Console.WriteLine("Creating CollaborationRequest object.");
                var collaboration = new CollaborationRequests
                {
                    LogId = requestDto.LogId,
                    RequestingTechnicianId = requestDto.RequestingTechnicianId,
                    InvitedTechnicianId = requestDto.InvitedTechnicianId,
                    Status = "PENDING",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                // Add collaboration request to the database
                Console.WriteLine("Adding collaboration request to the database.");
                await _dbContext.CollaborationRequests.AddAsync(collaboration);

                // Save changes to the database
                Console.WriteLine("Saving changes to the database.");
                await _dbContext.SaveChangesAsync();

                // Log success
                Console.WriteLine("Collaboration request saved successfully.");
                return Ok(new { Message = "Collaboration request sent successfully!" });
            }
            catch (DbUpdateException dbEx)
            {
                // Handle database-related exceptions
                Console.WriteLine($"Database error: {dbEx.Message}");
                return StatusCode(500, $"Database error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                // Log other exceptions
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("Respond/{collaborationId}")]
        public async Task<IActionResult> RespondToCallaboration(int collaborationId, [FromBody] CollaborationRequests responseDto)
        {
            if (responseDto == null)
                return BadRequest("Response data is required.");

            var collaboration = await _dbContext.CollaborationRequests.FindAsync(collaborationId);
            if (collaboration == null)
                return NotFound("Collaboration request not found.");

            if (collaboration.Status != "PENDING")
                return BadRequest("This collaboration request has already been processed.");

            collaboration.Status = responseDto.Status.ToUpper();
            collaboration.UpdatedAt = DateTime.UtcNow;

            _dbContext.CollaborationRequests.Update(collaboration);
            await _dbContext.SaveChangesAsync();

            return Ok(new { Message = $"Collaboration request {responseDto.Status.ToLower()}!" });
        }

        // GET: api/Collaboration/GetRequests?technicianId=3
        [HttpGet("GetRequests")]
        public async Task<IActionResult> GetCollaborationRequests(int technicianId)
        {
            var requests = await _dbContext.CollaborationRequests
                .Where(cr => cr.InvitedTechnicianId == technicianId)
                .Include(cr => cr.RequestTech)
                .ToListAsync();

            var response = requests.Select(r => new
            {
                r.CollaboratedId,
                r.LogId,
                RequestingTechnicianName = r.RequestTech.Surname,
                r.Status,
                r.CreatedAt
            });

            return Ok(response);
        }
    }
}
