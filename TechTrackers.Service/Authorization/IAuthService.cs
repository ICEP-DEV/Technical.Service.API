using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTrackers.Data.Model;

namespace TechTrackers.Service.Authorization
{
    public interface IAuthService
    {
        Task<User> SignUp(User user);
        Task<User> Login(string email, string password);


    }
}
