using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechTrackers.Data;
using TechTrackers.Data.Model;
using TechTrackers.Data.Model.dto;

namespace TechTrackers.Service.Administrator
{
    public class AdministratorService : IAdministratorService
    {
        private readonly TechTrackersDbContext _dbContext;
        private readonly INotifyService _notifyService;

        // Dependency injection with INotifyService
        public AdministratorService(TechTrackersDbContext dbContext, INotifyService notifyService)
        {
            _dbContext = dbContext;
            _notifyService = notifyService;
        }

        // Method to add an SLA based on priority level
        public async Task<SLA> AddSLA(string priority)
        {
            var existingSLA = await _dbContext.SLAs
                .FirstOrDefaultAsync(s => s.PriorityLevel == priority);

            if (existingSLA != null)
            {
                return null; // SLA with this priority already exists
            }

            var sla = new SLA
            {
                PriorityLevel = priority,
                Description = priority switch
                {
                    "CRITICAL" => "No wait response required for critical issues only.",
                    "HIGH" => "Immediate response required for high-priority issues.",
                    "MEDIUM" => "Timely response within 1-2 business days for medium priority.",
                    "LOW" => "Resolution within 3-5 business days for low priority.",

                    _ => throw new ArgumentException("Invalid priority level specified.")
                },
                ResolutionTimeframe = priority switch
                {
                    "CRITICAL" => 1,
                    "HIGH" => 4,
                    "MEDIUM" => 48,
                    "LOW" => 120,
                    _ => throw new ArgumentException("Invalid priority level specified.")
                }
            };

            await _dbContext.SLAs.AddAsync(sla);
            await _dbContext.SaveChangesAsync();
            return sla;
        }

        // Method to assign an SLA to a log
        public async Task<RespondWrapper> AssignSLAToLog(int slaId, int logId)
        {
            // Check if log exists
            var log = await _dbContext.Logs.FindAsync(logId);
            if (log == null)
            {
                return new RespondWrapper
                {
                    IsSuccess = false,
                    Message = $"Log with ID {logId} not found."
                };
            }

            // Check if SLA exists
            var sla = await _dbContext.SLAs.FindAsync(slaId);
            if (sla == null)
            {
                return new RespondWrapper
                {
                    IsSuccess = false,
                    Message = $"SLA with ID {slaId} not found."
                };
            }

            // Check if SLA is already assigned
            if (log.SLAId == slaId)
            {
                return new RespondWrapper
                {
                    IsSuccess = false,
                    Message = $"Log {logId} already has SLA {slaId} assigned."
                };
            }

            // Assign SLA and handle notification errors
            log.SLAId = slaId;
            await _dbContext.SaveChangesAsync();

            // Send confirmation notification with error handling
            try
            {
                await _notifyService.CreateNotification(
                    log.TechnicianId ?? 3,
                    log.LogId,
                    $"SLA {slaId} assigned to Log {logId}.",
                    "INFORMATION"
                );
            }
            catch (Exception ex)
            {
                return new RespondWrapper
                {
                    IsSuccess = false,
                    Message = $"SLA assigned, but notification failed: {ex.Message}"
                };
            }

            return new RespondWrapper
            {
                IsSuccess = true,
                Message = $"SLA {slaId} successfully assigned to Log {logId}.",
                Result = log
            };
        }

        // Method to check and handle SLA compliance
        public async Task<RespondWrapper> CheckAndHandleSLACompliance(int logId)
        {
            try
            {
                // Fetch unresolved log and include its SLA for compliance check
                var log = await _dbContext.Logs
                    .Include(log => log.SLA)
                    .FirstOrDefaultAsync(log => log.LogId == logId && log.LogStatus != "RESOLVED");

                // Handle log not found or already resolved
                if (log == null)
                {
                    return new RespondWrapper
                    {
                        IsSuccess = false,
                        Message = $"Error: Log with ID {logId} not found or is already resolved."
                    };
                }

                // Handle case where no SLA is assigned to the log
                if (log.SLAId == null)
                {
                    return new RespondWrapper
                    {
                        IsSuccess = false,
                        Message = $"Error: No SLA assigned to Log {logId}. Please assign an SLA first."
                    };
                }

                var sla = log.SLA;
                var elapsed = DateTime.UtcNow - log.CreatedAt;
                var slaTimeLimit = TimeSpan.FromHours(sla.ResolutionTimeframe);

                // Send reminder at 50% elapsed time with error handling
                if (elapsed >= slaTimeLimit * 0.5 && elapsed < slaTimeLimit)
                {
                    var reminderResponse = await SendReminderNotification(log);
                    if (!reminderResponse.IsSuccess)
                    {
                        return new RespondWrapper
                        {
                            IsSuccess = false,
                            Message = $"Reminder Notification Error: {reminderResponse.Message}"
                        };
                    }
                }

                // Escalate if SLA breached, with detailed error message if escalation fails
                if (elapsed >= slaTimeLimit)
                {
                    var escalationResponse = await EscalateLog(log);
                    if (!escalationResponse.IsSuccess)
                    {
                        return new RespondWrapper
                        {
                            IsSuccess = false,
                            Message = $"Escalation Error: {escalationResponse.Message}"
                        };
                    }

                    return new RespondWrapper
                    {
                        IsSuccess = false,
                        Message = $"SLA breached for Log {logId}. Escalation procedures initiated."
                    };
                }

                // SLA compliance met, return success message
                return new RespondWrapper
                {
                    IsSuccess = true,
                    Message = $"Log {logId} is within SLA compliance. Time remaining until breach: {slaTimeLimit - elapsed:g}."
                };
            }
            catch (Exception ex)
            {
                // Catch any unhandled errors and report with detailed message
                return new RespondWrapper
                {
                    IsSuccess = false,
                    Message = $"Unexpected error during SLA compliance check: {ex.Message}"
                };
            }
        }

        // Enhanced Reminder Notification with Error Reporting
        private async Task<RespondWrapper> SendReminderNotification(Log log)
        {
            int[] recipients = { log.TechnicianId ?? 0, log.StaffId };

            foreach (var recipientId in recipients)
            {
                if (recipientId > 0)
                {
                    try
                    {
                        await _notifyService.CreateNotification(
                            recipientId,
                            log.LogId,
                            $"Reminder: Log {log.LogId} is nearing SLA breach.",
                            "WARNING"
                        );
                    }
                    catch (Exception ex)
                    {
                        return new RespondWrapper
                        {
                            IsSuccess = false,
                            Message = $"Failed to send reminder to User {recipientId} for Log {log.LogId}. Error: {ex.Message}"
                        };
                    }
                }
            }

            return new RespondWrapper
            {
                IsSuccess = true,
                Message = $"Reminder notifications sent successfully for Log {log.LogId}."
            };
        }

        // Enhanced Escalation Notification with Error Reporting
        private async Task<RespondWrapper> EscalateLog(Log log)
        {
            try
            {
                // Notify HOD about SLA breach
                var hod = await _dbContext.Users.FirstOrDefaultAsync(u => u.Role.RoleName == "HOD");
                if (hod != null)
                {
                    await _notifyService.CreateNotification(
                        hod.UserId,
                        log.LogId,
                        $"Escalation: Log {log.LogId} breached its SLA.",
                        "ALERT"
                    );
                }

                // Notify technician and staff involved in the log
                int[] usersToNotify = { log.TechnicianId ?? 0, log.StaffId };
                foreach (var userId in usersToNotify)
                {
                    if (userId > 0)
                    {
                        try
                        {
                            await _notifyService.CreateNotification(
                                userId,
                                log.LogId,
                                $"Log {log.LogId} has been escalated due to SLA breach.",
                                "ALERT" 
                            );
                        }
                        catch (Exception ex)
                        {
                            return new RespondWrapper
                            {
                                IsSuccess = false,
                                Message = $"Failed to notify User {userId} for Log {log.LogId} escalation. Error: {ex.Message}"
                            };
                        }
                    }
                }

                return new RespondWrapper
                {
                    IsSuccess = true,
                    Message = $"Escalation notifications sent successfully for Log {log.LogId}."
                };
            }
            catch (Exception ex)
            {
                return new RespondWrapper
                {
                    IsSuccess = false,
                    Message = $"Unexpected error during escalation for Log {log.LogId}: {ex.Message}"
                };
            }
        }

    }
}
