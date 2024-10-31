using TechTrackers.Data;
using TechTrackers.Data.Model;

namespace TechTrackers.Service
{
    public class TechTrackerService : ITechTrackerService
    {
        private readonly TechTrackersDbContext _dbContext;

        public TechTrackerService(TechTrackersDbContext dbContext)
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

    }
}
