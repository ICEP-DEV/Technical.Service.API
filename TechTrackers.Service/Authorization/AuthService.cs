using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTrackers.Data;
using TechTrackers.Data.Model;

namespace TechTrackers.Service.Authorization
{
    public class AuthService: IAuthService
    {
        private readonly TeckTrackersDbContext _dbContext;

        public AuthService(TeckTrackersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> SignUp(User user)
        {
           user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            // If user is not found or password does not match
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            return user; // Login successful
        }

        
    }
}
