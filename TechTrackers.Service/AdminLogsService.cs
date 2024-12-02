using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTrackers.Data;
using TechTrackers.Data.Model.dto;

namespace TechTrackers.Service
{
    public class AdminLogsService
    {
        private readonly TechTrackersDbContext _dbContext;

        public AdminLogsService(TechTrackersDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        private string GetDepartmentInitials(string departmentName, int logId)
        {
            if (string.IsNullOrEmpty(departmentName))
                return $"LOG-{logId:D4}";

            // Extract initials by taking the first letter of each word
            var initials = string.Join("", departmentName.Split(' ').Select(word => word[0])).ToUpper();
            return $"{initials}-{logId:D4}";
        }
        public async Task<IEnumerable<AdminLogDto>> DispalyAllLogsAdmin()
        {

            try
            {
                var query = _dbContext.Logs
                    .Include(log => log.Staff)
                    .ThenInclude(staff => staff.Department)
                    .Include(log => log.Technician)
                    .Include(log => log.Category)
                    .AsQueryable();

                var logs = await query.ToListAsync();

                // Transform the data after fetching it
                var logDetails = logs.Select(log => new AdminLogDto
                {
                    LogId = log.LogId,
                    IssueId = GetDepartmentInitials(log.Staff?.Department?.DepartmentName, log.LogId),
                    IssueTitle = log.IssueTitle, 
                    CategoryName = log.Category?.CategoryName,
                    IssuedAt = log.CreatedAt,
                    Priority = log.Priority,
                    Department = log.Staff?.Department?.DepartmentName,
                    Status = log.LogStatus ?? "PENDING",
                    Description = log.Description,
                    Location = log.Location,
                    //AttachmentFile = log.AttachmentFile,
                    AssignedTo = log.Technician != null ? $"{log.Technician.Initials} {log.Technician.Surname}" : "Unassigned",
                    LogBy = log.Staff != null ? log.Staff.Initials + " " + log.Staff.Surname : null,
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

        public async Task<List<AdminLogDto>> GetAdminLoggedIssuesAsync(int adminId)
        {
            return await _dbContext.Logs
                .Where(log => log.StaffId == adminId) // Filter logs by userId
                .Select(log => new AdminLogDto
                {
                    LogId = log.LogId,
                    IssueId = $"{log.Staff.Department.DepartmentName}-{log.LogId}", // Combine department name and log ID
                    AssignedTo = $"{log.Technician.Surname}-{log.Technician.Initials}",
                    LogBy = $"{log.Staff.Surname} {log.Staff.Initials}",
                    Location = log.Location,
                    CategoryName = log.Category.CategoryName,
                    IssuedAt = log.CreatedAt,
                    Department = log.Staff.Department.DepartmentName,
                    Priority = log.Priority,
                    Status = log.LogStatus ?? "PENDING",
                    IssueTitle = log.IssueTitle,
                    AttachmentBase64 = log.AttachmentFile != null
                        ? Convert.ToBase64String(log.AttachmentFile)
                        : null
                })
                .ToListAsync();
        }


    }
}
