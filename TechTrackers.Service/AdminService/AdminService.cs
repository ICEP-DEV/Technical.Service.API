using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTrackers.Data;
using TechTrackers.Data.Model;
using TechTrackers.Data.Model.dto;

namespace TechTrackers.Service.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly TechTrackersDbContext _dbContext;

        public AdminService(TechTrackersDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<SLA> AddSLA(string priority)
        {
            var existingSLA = await _dbContext.SLAs
                                .FirstOrDefaultAsync(s => s.PriorityLevel == priority);

            if (existingSLA != null)
            {
                throw new InvalidOperationException($"An SLA with priority '{priority}' already exists.");
            }

            var sla = new SLA
            {
                PriorityLevel = priority,
                // You can add other properties here if needed
            };

            await _dbContext.SLAs.AddAsync(sla);
            await _dbContext.SaveChangesAsync();

            return sla;
        }



        public async Task<bool> AssignSLAToLog(int slaId, int logId)
        {
            var log = await _dbContext.Logs.FindAsync(logId);
            if (log == null) return false; // Log not found

            log.SLAId = slaId; // Assign SLA to log
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckAndHandleSLACompliance(SLAComplianceDto complianceDto)
        {
            // Calculate the allowed time by adding the resolution timeframe to the log's created date
            var allowedTime = complianceDto.CreatedAt.AddHours(complianceDto.ResolutionTimeframe);

            // Check if the current time has exceeded the allowed time
            if (DateTime.UtcNow > allowedTime)
            {
                // Retrieve HOD or ADMIN role
                var role = await _dbContext.Roles
                    .FirstOrDefaultAsync(r => r.RoleName == "HOD" || r.RoleName == "ADMIN");

                if (role != null)
                {
                    // Retrieve user associated with the HOD or ADMIN role
                    var hodUser = await _dbContext.Users
                        .FirstOrDefaultAsync(u => u.RoleId == role.RoleId);

                    if (hodUser != null)
                    {
                        // Create an escalation due to SLA breach
                        var escalation = new Escalation
                        {
                            LogId = complianceDto.LogId,
                            HODId = hodUser.UserId, // Set HOD's User ID for escalation
                            Reason = "SLA breached due to non-compliance.",
                            Timestamp = DateTime.UtcNow
                        };

                        await _dbContext.Escalations.AddAsync(escalation);

                        // Create a notification for the HOD user
                        var notification = new Notification
                        {
                            LogId = complianceDto.LogId,
                            UserId = hodUser.UserId,
                            Message = $"SLA breached for Log ID {complianceDto.LogId}.",
                            Type = "ALERT",
                            Timestamp = DateTime.UtcNow,
                            ReadStatus = false
                        };

                        await _dbContext.Notifications.AddAsync(notification);
                        await _dbContext.SaveChangesAsync();
                        return true; // SLA breached and escalated
                    }
                    else
                    {
                        throw new Exception("HOD or ADMIN user not found.");
                    }
                }
                else
                {
                    throw new Exception("HOD or ADMIN role not found.");
                }
            }

            return false; // SLA is still in compliance
        }

    }
}
