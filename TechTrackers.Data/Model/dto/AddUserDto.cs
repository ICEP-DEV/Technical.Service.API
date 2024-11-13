using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model.dto
{
    public class AddUserDto
    {
        public string? surname { get; set; }
        public string? initials { get; set; }
        public string? emailAddress { get; set; }
        public int departmentId { get; set; }
        public int roleId { get; set; }
        public string? passwordHash { get; set; }
    }
}
