using TechTrackers.Data;
using TechTrackers.Data.Model;

namespace TechTrackers.Service
{
    public class TechTrackerService : ITechTrackerService
    {
        private readonly TeckTrackersDbContext _dbContext;

        public TechTrackerService(TeckTrackersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //User
        public async Task<User> RegisterUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
        public IEnumerable<User> GetUsers()
        {
            return _dbContext.Users;
        }


        //Notification
        public async Task<Notifications> SendNotification(Notifications notifications)
        {
            await _dbContext.Notifications.AddAsync(notifications);
            await _dbContext.SaveChangesAsync();
            return notifications;
        }

        public IEnumerable<Notifications> GetNotification()
        {
            return _dbContext.Notifications.ToList();
        }

    }
}
