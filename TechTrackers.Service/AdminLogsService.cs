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
                return $"LOG-{logId:D3}";

            // Extract initials by taking the first letter of each word
            var initials = string.Join("", departmentName.Split(' ').Select(word => word[0])).ToUpper();
            return $"{initials}-{logId:D3}";
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
                    IssueId = GetDepartmentInitials(log.Staff?.Department?.DepartmentName, log.LogId),
                    CategoryName = log.Category?.CategoryName,
                    IssuedAt = log.CreatedAt,
                    Priority = log.Priority,
                    Department = log.Staff?.Department?.DepartmentName,
                    Status = log.LogStatus ?? "PENDING",
                    Description = log.Description,
                    Location = log.Location,
                    AttachmentUrl = log.AttachmentUrl,
                    AssignedTo = log.Technician != null ? $"{log.Technician.Initials} {log.Technician.Surname}" : "Unassigned",
                    LogBy = log.Staff != null ? log.Staff.Initials + " " + log.Staff.Surname : null,
                    
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

    }
}
