using Microsoft.EntityFrameworkCore;
using TechTrackers.Data;
using TechTrackers.Data.Model;
using TechTrackers.Data.Model.dto;

namespace TechTrackers.Service
{
    public class LogService
    {
        private readonly TechTrackersDbContext _dbContext;

        public LogService(TechTrackersDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        // Method to fetch all the logs
        public async Task<IEnumerable<LogDetailDto>> GetAllLogsAsync()
        {
            return await _dbContext.Logs
               .Include(log => log.Technician) // Include the Technician (user with a technician role)
               .ThenInclude(technician => technician.Department) // Include Technician's Department
               .Include(log => log.Category) // Include the log's category
               .Select(log => new LogDetailDto
               {
                   IssueId = $"{log.Technician.Department.Department_Name}-{log.Log_ID}",
                   AssignedTo = log.Technician != null ? log.Technician.Surname + " " + log.Technician.Initials : null, // Show the technician's name
                   CategoryName = log.Category.Category_Name,
                   IssuedAt = log.Created_At,
                   Department = log.Technician != null ? log.Technician.Department.Department_Name : null, // Technician's department
                   Priority = log.Priority,
                   Status = log.Log_Status,
                   Description = log.Description,
                   AttachmentUrl = log.Attachment_URL
               })
               .ToListAsync();
        }

        // Method for creating a new log entry
        public async Task<Log> LogIssue(int staffId, LogDto logDto)
        {
            // Validate staff before logging issue
            var staff = await _dbContext.Users
                .FirstOrDefaultAsync(user => user.User_ID == staffId && user.UserRoles.Any(ur => ur.Role.Role_Name == "Staff"));

            if (staff == null)
            {
                throw new KeyNotFoundException($"Staff with ID {staffId} not found.");
            }

            // Ensure technician exists
            var technician = await _dbContext.Users
                .FirstOrDefaultAsync(user => user.User_ID == logDto.Technician_ID && user.UserRoles.Any(ur => ur.Role.Role_Name == "Technician"));

            if (technician == null)
            {
                throw new KeyNotFoundException($"Technician with ID {logDto.Technician_ID} not found.");
            }

            var sla = await _dbContext.Service_Level_Agreements.FindAsync(logDto.SLA_ID);
            if(sla == null)
            {
                throw new KeyNotFoundException($"SLA with ID {logDto.SLA_ID} not found.");
            }
            // Parse and validate the Due_Date
           

            var log = new Log
            {
                Description = logDto.Description,
                Priority = logDto.Priority,
                Assigned_At = logDto.Assigned_at.HasValue? logDto.Assigned_at.Value.ToString("o") : null,
                Log_Status = logDto.Log_Status,
                Staff_ID = staffId,
                Technician_ID = logDto.Technician_ID, // Validated technician
                Category_ID = logDto.Category_ID,
                Created_At = DateTime.Now,

               /*escription = logDto.Description,
                Priority = logDto.Priority,
                Assigned_At = logDto.Assigned_at,
                Staff_ID = staffId,
                //Technician_ID = logDto.Technician_ID, // Validated technician
                Category_ID = logDto.Category_ID,
                Created_At = logDto.Created_at,
                Attachment_URL = logDto.AttechmentUrl,*/

            };

            await _dbContext.Logs.AddAsync(log);
            await _dbContext.SaveChangesAsync();
            return log;
        }
    }
}
