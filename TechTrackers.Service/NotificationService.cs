using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTrackers.Data;
using TechTrackers.Data.Model;

namespace TechTrackers.Service
{
    public class NotificationService : INotificationService
    {
        private readonly TechTrackersDbContext _dbContext;

        public NotificationService(TechTrackersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Notifications> SendNotification(Notifications notifications)
        {
            await _dbContext.Notifications.AddAsync(notifications);
            await _dbContext.SaveChangesAsync();
            return notifications;
        }

        public IEnumerable<Notifications> GetNotifications()
        {
            return _dbContext.Notifications.ToList();
        }
    }
}
