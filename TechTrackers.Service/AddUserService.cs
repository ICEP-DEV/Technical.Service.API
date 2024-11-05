using Microsoft.EntityFrameworkCore;
using TechTrackers.Data;
using TechTrackers.Data.Model;

namespace TechTrackers.Service
{
    public class AddUserService : IAddUserService
    {
        private readonly TechTrackersDbContext _dbContext;

        public AddUserService(TechTrackersDbContext dbContext)
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
        public async Task<User> GetUserById(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID: {id} doest not exist.");
            }
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            var exixtingUser = await _dbContext.Users.FindAsync(user.UserId);
            if (exixtingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {user.UserId} does not exist.");
            }

            exixtingUser.Surname = user.Surname;
            exixtingUser.Initials = user.Initials;
            exixtingUser.EmailAddress = user.EmailAddress;
            exixtingUser.PasswordHash = user.PasswordHash;

            await _dbContext.SaveChangesAsync();
            return exixtingUser;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null) return false;
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;

        }
        /*
        //Technician
        public async Task AddTechnicianAvailability(Technician_Availability availability)
        {
            _dbContext.technician_Availabilities.Add(availability);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async void Availability(Technician_Availability availability)
        {
            // Validate that the availability object is not null
            if (availability == null)
            {
                throw new ArgumentNullException(nameof(availability), "Availability cannot be null.");
            }

            // Validate that the technician exists in the system
            var technician = _dbContext.Users.FirstOrDefault(t => t.User_ID == availability.Technician_ID);
            if (technician == null)
            {
                throw new Exception($"Technician with ID {availability.Technician_ID} not found.");
            }

            // Validate the availability time range (from and to times)
            if (availability.From_Time >= availability.To_Time)
            {
                throw new Exception("The start time must be earlier than the end time.");
            }

            // Check if there's an overlap with any existing availability for this technician
            var overlappingAvailability = _dbContext.technician_Availabilities
                .Where(a => a.Technician_ID == availability.Technician_ID
                        && a.From_Time < availability.To_Time
                        && a.To_Time > availability.From_Time)
                .FirstOrDefault();

            if (overlappingAvailability != null)
            {
                throw new Exception("The specified availability overlaps with an existing time range.");
            }

            // Add the availability to the database

            _dbContext.technician_Availabilities.Add(availability);
            await _dbContext.SaveChangesAsync();
        }
        */
    }
}
