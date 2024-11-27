using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private string GetDepartmentInitials(string departmentName, int userIssueId)
        {
            if (string.IsNullOrEmpty(departmentName))
                return $"LOG-{userIssueId:D4}";

            // Extract initials by taking the first letter of each word
            var initials = string.Join("", departmentName.Split(' ').Select(word => word[0])).ToUpper();
            return $"{initials}-{userIssueId:D4}";
        }
        public async Task<IEnumerable<LogDetailDto>> GetAllLogsAsync(int? userId, bool isTechnician)
        {

            try
            {

                var query = _dbContext.Logs
                    .Include(log => log.Staff)
                    .ThenInclude(staff => staff.Department)
                    .Include(log => log.Technician)
                    .Include(log => log.Category)
                    .AsQueryable();

                if (userId.HasValue && !isTechnician)
                {
                    query = query.Where(log => log.StaffId == userId.Value);
                }

                // Filter by TechnicianId if the user is a Technician
                if (userId.HasValue && isTechnician)
                {
                    query = query.Where(log => log.TechnicianId == userId.Value);
                }


                var logs = await query.ToListAsync();

                // Transform the data after fetching it
                var logDetails = logs.Select(log => new LogDetailDto
                {
                    IssueId = GetDepartmentInitials(log.Staff?.Department?.DepartmentName, log.UserIssueId),
                    CategoryName = log.Category?.CategoryName,
                    IssuedAt = log.CreatedAt.ToString("yyyy-MM-dd hh:mm tt"),
                    Priority = log.Priority,
                    IssueTitle = log.IssueTitle,
                    Department = log.Staff?.Department?.DepartmentName,
                    Status = log.LogStatus ?? "PENDING",
                    Description = log.Description,
                    Location = log.Location,
                    //AttachmentUrl = log.AttachmentUrl,
                    AssignedTo = log.Technician != null ? $"{log.Technician.Initials} {log.Technician.Surname}" : "Unassigned",

                    AttachmentBase64 = log.AttachmentFile != null
                        ? Convert.ToBase64String(log.AttachmentFile)
                        : null
                });

                return logDetails;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching logs: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
                throw;
            }
        }

        // Method for creating a new log entry
        public async Task<Log> LogIssue(LogDto logDto)
        {
            try
            {
                // Retrive the SLA based on the priority level
                var sla = await _dbContext.SLAs
                    .FirstOrDefaultAsync(s => s.PriorityLevel == logDto.Priority);

                if (sla == null)
                {
                    throw new Exception($"SLA not found for the selected priority level: {logDto.Priority}.");
                }

                byte[]? fileData = null;
                if (logDto.AttachmentFile != null)
                {
                    Console.WriteLine($"Received file with name: {logDto.AttachmentFile.FileName}");
                    using (var ms = new MemoryStream())
                    {
                        await logDto.AttachmentFile.CopyToAsync(ms);
                        fileData = ms.ToArray();
                    }
                }
                else
                {
                    Console.WriteLine("No file received in the request.");
                }

                //Finding the highest UserIssueId for the current User
                var maxUserIssuedId = await _dbContext.Logs
                    .Where(l => l.StaffId == logDto.Staff_ID)
                    .MaxAsync(l => (int?)l.UserIssueId) ?? 0;

                //Setting the new  UserIssuedId to be max + 1
                var newUserIssueId = maxUserIssuedId + 1;

                // Log SLA lookup success
                Console.WriteLine($"SLA found: SLA_ID = {sla.SLAId}, Response_Time = {sla.ResponseTimeframe}, Resolution_Time = {sla.ResolutionTimeframe}");

                var log = new Log
                {
                    UserIssueId = newUserIssueId,
                    IssueTitle = logDto.Issue_Title,
                    Description = logDto.Description,
                    Priority = logDto.Priority ?? "MEDIUM",
                    Location = logDto.Location ?? "Not specified",
                    CategoryId = logDto.Category_ID,
                    LogStatus = logDto.LogStatus = "PENDING",
                    AttachmentFile = fileData,
                    StaffId = logDto.Staff_ID,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    SLAId = sla.SLAId

                };

                await _dbContext.Logs.AddAsync(log);
                await _dbContext.SaveChangesAsync();
                // Log success
                Console.WriteLine($"Log successfully created with UserIssueId: {log.UserIssueId}");

                var notification = new Notification
                {
                    LogId = log.LogId,
                    UserId = log.StaffId, // Assuming staff user needs to be notified
                    Message = $"Your issue has been logged successfully with reference ID: {GetDepartmentInitials(logDto.Department, log.LogId)}",
                    Type = "INFORMATION",
                    Timestamp = DateTime.Now,
                    ReadStatus = false
                };

                await _dbContext.Notifications.AddAsync(notification);
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
                throw new Exception("Failed to log issue due to server error.", ex);
            }
        }



        //NOTIFICATIONS 

        public async Task<IEnumerable<NotificationDto>> GetNotificationsAsync(int userId, bool onlyUnread = false)
        {
            try
            {
                var query = _dbContext.Notifications
                    .Where(n => n.UserId == userId)
                    .AsQueryable();

                if (onlyUnread)
                {
                    query = query.Where(n => !n.ReadStatus);
                }

                var notifications = await query.OrderByDescending(n => n.Timestamp).ToListAsync();

                // Transform data into a DTO
                var notificationDtos = notifications.Select(n => new NotificationDto
                {
                    NotificationId = n.NotificationId,
                    LogId = n.LogId,
                    UserId = n.UserId,
                    Message = n.Message,
                    Type = n.Type,
                    Timestamp = n.Timestamp,
                    ReadStatus = n.ReadStatus
                });

                return notificationDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching notifications: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw new Exception("Failed to retrieve notifications due to server error.", ex);
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

