using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TechTrackers.Data.Model;
using TechTrackers.Data.Model.dto;
using TechTrackers.Service;

namespace TechTrackers.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("corspolicy")]
    public class UsersController : ControllerBase
    {
        private readonly IAddUserService _userService;

        public UsersController(IAddUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTechnician([FromBody] TechnicianDto technicianDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Create new technician
            var newTechnician = new User
            {
                Surname = technicianDto.FullName,
                EmailAddress = technicianDto.Email,
                PasswordHash = technicianDto.Password
            };



            await _userService.RegisterUser(newTechnician);

            // Add technician-specific details
            var technicianDetails = new Technician
            {
                UserId = newTechnician.UserId,
                Specialization = technicianDto.Specialization,
                Contact = technicianDto.Contact,
                FromTime = technicianDto.FromTime,
                ToTime = technicianDto.ToTime
            };

            await _userService.AddTechnicianAvailability(technicianDetails);
            return Ok(new
            {
                Message = "Technician added successfully",
                TechnicianId = newTechnician.UserId
            });
        }

        [HttpPost("AddAvailability")]
        public async Task<IActionResult> AddAvailability([FromBody] AvailabilityDto availabilityDto)
        {
            if (availabilityDto == null)
            {
                return BadRequest("Request body is missing or invalid.");
            }


            try
            {
                // Validate TechnicianId if necessary
                if (availabilityDto.TechnicianID <= 0)
                {
                    return BadRequest("TechnicianId is required and must be a valid ID.");
                }

                // Additional validation to ensure FromTime is earlier than ToTime
                if (availabilityDto.FromTime >= availabilityDto.ToTime)
                {
                    return BadRequest("FromTime must be earlier than ToTime.");
                }

                // Business logic to save the availability data in the database
                var technicianAvailability = new Technician
                {
                    TechnicianId = availabilityDto.TechnicianID,
                    Specialization = availabilityDto.Specialization,
                    Contact = availabilityDto.Contact,
                    FromTime = availabilityDto.FromTime,
                    ToTime = availabilityDto.ToTime
                };

                await _userService.AddTechnicianAvailability(technicianAvailability);
                await _userService.SaveChangesAsync();

                return Ok("Availability added successfully.");
            }
            catch (FormatException ex)
            {
                // This will handle cases where FromTime or ToTime cannot be parsed into TimeSpan
                return BadRequest("Error parsing time. Ensure the time format is HH:mm:ss.");
            }
            catch (Exception ex)
            {
                // General error handling
                return StatusCode(500, "An unexpected error occurred. Please try again.");
            }

        }

        //Get all Users
        [HttpGet]
        public IActionResult GetUser()
        {
            var respondWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to fetch user."
            };

            var results = _userService.GetUsers();

            if (results.Count() > 0)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to update user"
            };

            var updatedUser = await _userService.UpdateUser(user);

            if (updatedUser != null)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "User updated successfully!",
                    Result = updatedUser
                };
            }

            return Ok(responseWrapper);
        }

        // Delete User
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to delete User"
            };

            var isDeleted = await _userService.DeleteUser(id);

            if (isDeleted)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "User deleted successfully!"
                };
            }

            return Ok(responseWrapper);
        }

        //Get User by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "User not found"
            };

            var user = await _userService.GetUserById(id);

            if (user != null)
            {
                responseWrapper = new RespondWrapper
                {
                    IsSuccess = true,
                    Message = "Successfully retrieved user",
                    Result = user
                };
            }

            return Ok(responseWrapper);
        }
    }

}
