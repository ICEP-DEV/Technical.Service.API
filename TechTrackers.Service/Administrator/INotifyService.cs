using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTrackers.Data.Model;

namespace TechTrackers.Service.Administrator
{
    public interface INotifyService
    {
        Task<Notification> SendNotification(Notification notification);
        Task<Notification> CreateNotification(int userId, int logId, string message, string type);
        IEnumerable<Notification> GetNotifications();
    }
}
