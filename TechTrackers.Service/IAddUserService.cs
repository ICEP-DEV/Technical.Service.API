using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTrackers.Data.Model;
using TechTrackers.Data.Model.dto;

namespace TechTrackers.Service
{
    public interface IAddUserService
    {
        //Users
        Task<User> RegisterUser(User user);
        IEnumerable<User> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
        Task AddTechnicianAvailability(Technician technicianDetais);
        Task SaveChangesAsync();

        void Availability(Technician availability);
    }
}
