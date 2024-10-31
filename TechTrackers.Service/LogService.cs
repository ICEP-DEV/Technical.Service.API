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
               .Include(log => log.Staff)
               .ThenInclude(staff => staff.Department)
               .Include(log => log.Technician)
               .Include(log => log.Category) // Include the log's category
               .Select(log => new LogDetailDto
               {
                   IssueId = $"{log.Staff.Department.DepartmentName}-{log.LogId}",  // Simple Log ID format
                   CategoryName = log.Category.CategoryName,
                   IssuedAt = log.CreatedAt,
                   Priority = log.Priority,
                   Department = log.Staff != null ? log.Staff.Department.DepartmentName : null, // Staff's department
                   Status = log.LogStatus,
                   Description = log.Description,
                   Location = log.Location,
                   AttachmentUrl = log.AttachmentUrl,
                   AssignedTo = log.Technician != null ? $"{log.Technician.Initials} {log.Technician.Surname}" : "Unassigned" // Display technician's name
               })
               .ToListAsync();
        }

        // Method for creating a new log entry
        public async Task<Log> LogIssue(LogDto logDto)
        {
            try
            {
                var log = new Log
                {
                    Description = logDto.Description,
                    Priority = logDto.Priority ?? "MEDIUM",
                    Location = logDto.Location ?? "Not specified",
                    CategoryId = logDto.Category_ID,
                    //LogStatus = logDto.LogStatus = "PENDING",
                    AttachmentUrl = logDto.AttechmentUrl,
                    StaffId = logDto.Staff_ID,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                await _dbContext.Logs.AddAsync(log);
                await _dbContext.SaveChangesAsync();
                return log;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating log: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }



        /* return await _dbContext.Logs
            .Include(log => log.Technician) // Include the Technician (user with a technician role)
            .ThenInclude(technician => technician.Department) // Include Technician's Department
            .Include(log => log.Category) // Include the log's category
            .Select(log => new LogDetailDto
            {
                IssueId = $"{log.Technician.Department.DepartmentName}-{log.LogId}",
                AssignedTo = log.Technician != null ? log.Technician.Surname + " " + log.Technician.Initials : null, // Show the technician's name
                CategoryName = log.Category.CategoryName,
                IssuedAt = log.CreatedAt,
                Department = log.Technician != null ? log.Technician.Department.DepartmentName : null, // Technician's department
                Priority = log.Priority,
                Status = log.LogStatus,
                Description = log.Description,
                AttachmentUrl = log.AttachmentUrl
            })
            .ToListAsync();
     }

     // Method for creating a new log entry
     public async Task<Log> LogIssue( LogDto logDto)
     {
         // Validate staff before logging issue
        /* var staff = await _dbContext.Users
             .FirstOrDefaultAsync(user => user.UserId == staffId && user.UserRoles.Any(ur => ur.Role.RoleName == "Staff"));

         if (staff == null)
         {
             throw new KeyNotFoundException($"Staff with ID {staffId} not found.");
         }

         // Ensure technician exists
         /*var technician = await _dbContext.Users
             .FirstOrDefaultAsync(user => user.UserId == logDto.Technician_Id && user.UserRoles.Any(ur => ur.Role.Role_Name == "Technician"));

         if (technician == null)
         {
             throw new KeyNotFoundException($"Technician with ID {logDto.Technician_ID} not found.");
         }*/

        /*var sla = await _dbContext.Service_Level_Agreements.FindAsync(logDto.SLA_ID);
        if (sla == null)
        {
            throw new KeyNotFoundException($"SLA with ID {logDto.SLA_ID} not found.");
        }
        // Parse and validate the Due_Date


        var log = new Log
        {
            Description = logDto.Description,
            Priority = logDto.Priority,
            Location = logDto.Location,
            CategoryId = logDto.Category_ID,
            CreatedAt = DateTime.Now,
            AttachmentUrl = logDto.AttechmentUrl

        };

        await _dbContext.Logs.AddAsync(log);
        await _dbContext.SaveChangesAsync();
        return log;*/
    }
    }

