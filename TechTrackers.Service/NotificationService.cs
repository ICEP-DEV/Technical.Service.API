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

        public async Task<Notification> SendNotification(Notification notifications)
        {
            await _dbContext.Notifications.AddAsync(notifications);
            await _dbContext.SaveChangesAsync();
            return notifications;
        }

        public IEnumerable<Notification> GetNotifications()
        {
            return _dbContext.Notifications.ToList();
        }

        IEnumerable<Notification> INotificationService.GetNotifications()
        {
            throw new NotImplementedException();
        }
    }
}
