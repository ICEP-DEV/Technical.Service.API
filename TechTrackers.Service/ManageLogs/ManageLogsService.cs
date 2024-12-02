using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTrackers.Data;
using TechTrackers.Data.Model.dto;

namespace TechTrackers.Service.ManageLogs
{
    public class ManageLogsService : IManageLogs
    {

        private readonly TechTrackersDbContext _dbContext;

        public ManageLogsService(TechTrackersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ChangeLogStatus(int logId, string newStatus)
        {
            var log = await _dbContext.Logs.FindAsync(logId);
            if (log == null) return false;

            log.LogStatus = newStatus.ToUpper(); // Ensure status is stored consistently
            log.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CloseLog(int logId)
        {
            var log = await _dbContext.Logs.FindAsync(logId);
            if (log == null) return false;

            log.LogStatus = "CLOSED";
            log.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<int> CountAllLogsAsync()
        {
            return await _dbContext.Logs.CountAsync();
        }

        public async Task<int> CountLogsByStatusAsync(string status)
        {
            return await _dbContext.Logs
                .CountAsync(log => log.LogStatus != null &&
                                   log.LogStatus.ToLower() == status.ToLower());
        }


        public async Task<IEnumerable<LogDetailDto>> GetOpenLogsAsync()
        {
            var logs = await _dbContext.Logs
                .Include(log => log.Staff)
                .ThenInclude(staff => staff.Department)
                .Include(log => log.Technician)
                .Include(log => log.Category)
                .Where(log => log.LogStatus == "OPEN")
                .ToListAsync();

            return logs.Select(log => new LogDetailDto
            {
                IssueId = $"LOG-{log.LogId:D4}",
                CategoryName = log.Category?.CategoryName,
                IssuedAt = log.CreatedAt.ToString("yyyy-MM-dd hh:mm tt"),
                Priority = log.Priority,
                Department = log.Staff?.Department?.DepartmentName,
                Status = log.LogStatus ?? "PENDING",
                Description = log.Description,
                Location = log.Location,
                AttachmentBase64 = log.AttachmentFile != null ? Convert.ToBase64String(log.AttachmentFile) : null,
                AssignedTo = log.Technician != null ? $"{log.Technician.Initials} {log.Technician.Surname}" : "Unassigned"
            });
        }

        public async  Task<bool> OpenLog(int logId)
        {
            var log = await _dbContext.Logs.FindAsync(logId);
            if (log == null) return false;

            log.LogStatus = "OPEN";
            log.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
