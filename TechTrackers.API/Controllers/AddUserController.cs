using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TechTrackers.Data.Model.dto;
using TechTrackers.Data.Model;
using TechTrackers.Service;

namespace TechTrackers.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("corspolicy")]
    public class AddUserController : ControllerBase
    {
        private readonly IAddUserService _addUserService;

        public AddUserController(IAddUserService addUserService)
        {
            _addUserService = addUserService;
        }

        //User
        //Add User
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            var respondWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to add user."
            };

            var results = await _addUserService.RegisterUser(user);

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
        /*
        //Add Technician
        [HttpPost]
        public IActionResult AddTechnician([FromBody] TechnicianDto technicianDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Create new technician
            var newTechnician = new User
            {
                Surname = technicianDto.FullName,
                Email = technicianDto.Email,
                Password = technicianDto.Password
            };


            _addUserService.RegisterUser(newTechnician);
            //_userService.SaveChanges();

            return Ok(new { TechnicianId = newTechnician.User_ID });
        }

        //Add Availability
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
                var technicianAvailability = new Technician_Availability
                {
                    Technician_ID = availabilityDto.TechnicianID,
                    Specialization = availabilityDto.Specialization,
                    Contact = availabilityDto.Contact,
                    From_Time = availabilityDto.FromTime,
                    To_Time = availabilityDto.ToTime
                };

                await _addUserService.AddTechnicianAvailability(technicianAvailability);
                await _addUserService.SaveChangesAsync();

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
        */
        
        //Get all Users
        [HttpGet]
        public IActionResult GetUser()
        {
            var respondWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to fetch user."
            };

            var results = _addUserService.GetUsers();

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

        // Update User
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            var responseWrapper = new RespondWrapper
            {
                IsSuccess = false,
                Message = "Unable to update user"
            };

            var updatedUser = await _addUserService.UpdateUser(user);

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

            var isDeleted = await _addUserService.DeleteUser(id);

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

            var user = await _addUserService.GetUserById(id);

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
