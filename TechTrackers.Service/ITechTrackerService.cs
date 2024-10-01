using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TechTrackers.Data.Model;

namespace TechTrackers.Service
{
    public interface ITechTrackerService
    {
        //Users
        Task<User> RegisterUser(User user);
        IEnumerable<User> GetUsers();

        //Notifications
        Task<Notifications> SendNotification(Notifications notifications);
        IEnumerable<Notifications> GetNotification();
    }
}
