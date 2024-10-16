using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTrackers.Data;
using TechTrackers.Data.Model;

namespace TechTrackers.Service.IssueLog
{
    public class LogService : ILogService
    {

        private readonly TeckTrackersDbContext _techtrackerDbContext;

        public LogService(TeckTrackersDbContext teckTrackerDbContext)
        {
            _techtrackerDbContext = teckTrackerDbContext;
        }


        public async Task<Log> LogIssue(Log log)
        {
            log.Created_At = DateTime.UtcNow;
            await _techtrackerDbContext.Logs.AddAsync(log);
            await _techtrackerDbContext.SaveChangesAsync();
            return log;
        }

        public IEnumerable<Log> GetAllLogs()
        {
            return _techtrackerDbContext.Logs.ToList();
        }


        public async Task<Log> GetLogById(int id)
        {

            var user = await _techtrackerDbContext.Logs.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {id} not found.");
            }

            return user;
        }

        public async Task<bool> DeleteLog(int id)
        {
            var log = await _techtrackerDbContext.Logs.FindAsync(id);

            if (log != null)
            {
                _techtrackerDbContext.Remove(log);
                await _techtrackerDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Log> UpdateLog(Log log)
        {

            var existingLog = await _techtrackerDbContext.Logs.FindAsync(log.Log_ID);

            if (existingLog == null)
            {
                throw new KeyNotFoundException($"Log with ID {log.Log_ID} not found.");

            }

            existingLog.Staff_ID = log.Staff_ID;
            existingLog.Category_ID = log.Category_ID;
            existingLog.Description = log.Description;
            existingLog.Attachment_URL = log.Attachment_URL;
            existingLog.Priority = log.Priority;
            existingLog.Created_At = log.Created_At;
            existingLog.Assigned_By = log.Assigned_By;
            existingLog.Assigned_At = log.Assigned_At;
            existingLog.Due_Date = log.Due_Date;
            existingLog.Technician_ID = log.Technician_ID;
            existingLog.Log_Status = log.Log_Status;
            existingLog.Updated_At = DateTime.UtcNow; // Set the updated timestamp
            existingLog.SLA_ID = log.SLA_ID;


            await _techtrackerDbContext.SaveChangesAsync();

            return existingLog;
        }

        public IEnumerable<Log> GetLogsByStatus(string status)
        {
            return _techtrackerDbContext.Logs.Where(log => log.Log_Status == status).ToList();
        }

        public IEnumerable<Log> GetLogsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _techtrackerDbContext.Logs
                .Where(log => log.Created_At >= startDate && log.Created_At <= endDate).
                ToList();
        }

        public int GetLogCount()
        {
            return _techtrackerDbContext.Logs.Count();
        }

        public async Task<bool> AssignLogToTechnician(int logId, int technicianId)
        {
            var log = await _techtrackerDbContext.Logs.FindAsync(logId);

            // If log is not found, return false
            if (log == null)
            {
                return false;
            }

            // Check if the technician exists and has the role of "Technician"
            var technicianExists = await _techtrackerDbContext.User_Roles
                .AnyAsync(ur => ur.User_ID == technicianId && ur.Role_ID == 4); // Assuming role 4 is for technicians

            // If the technician is not valid or doesn't exist, return false
            if (!technicianExists)
            {
                return false;
            }

            // Assign the technician to the log
            log.Technician_ID = technicianId;
            log.Updated_At = DateTime.UtcNow; // Update the timestamp

            // Save the changes
            await _techtrackerDbContext.SaveChangesAsync();

            return true;
        }



        public IEnumerable<Log> SearchLogs(string keyword)
        {
            return _techtrackerDbContext.Logs
                .Where(log => log.Description.Contains(keyword))
                .ToList();
        }

        /*public async Task<int> ArchiveOldLogs(int days)
        {
            var archiveDate = DateTime.UtcNow.AddDays(-days);
            var oldLogs = _techtrackerDbContext.Logs.Where(log => log.Created_At <= archiveDate).ToList();
            _techtrackerDbContext.Logs.RemoveRange(oldLogs);

            await _techtrackerDbContext.SaveChangesAsync();

            return oldLogs.Count;
        }*/
    }
}
