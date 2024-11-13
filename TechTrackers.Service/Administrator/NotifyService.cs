using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechTrackers.Data;
using TechTrackers.Data.Model;

namespace TechTrackers.Service.Administrator
{
    public class NotifyService : INotifyService
    {
        private readonly TechTrackersDbContext _dbContext;

        public NotifyService(TechTrackersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Notification> CreateNotification(int userId, int logId, string message, string type)
        {
            var notification = new Notification
            {
                UserId = userId,
                LogId = logId,
                Message = message,
                Type = type,
                Timestamp = DateTime.UtcNow,
                ReadStatus = false
            };
            return await SendNotification(notification);
        }

        public IEnumerable<Notification> GetNotifications()
        {
            return _dbContext.Notifications.ToList();
        }

        public async Task<Notification> SendNotification(Notification notification)
        {
            await _dbContext.Notifications.AddAsync(notification);
            await _dbContext.SaveChangesAsync();
            return notification;
        }
    }
}
