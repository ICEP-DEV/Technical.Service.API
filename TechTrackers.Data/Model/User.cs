using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_ID { get; set; }
        public string Surname { get; set; } = string.Empty;
        public string Initials { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        
        public int Department_ID { get; set; }

        [ForeignKey("Department_ID")]
        public Department? Department { get; set; }

        [ForeignKey("Role")]
        public int Role_ID { get; set; }

        public virtual ICollection<Log> CreatedLogs { get; set; } = new List<Log>();
        public virtual ICollection<Log> AssignedLogs { get; set; } = new List<Log>();
        public virtual ICollection<User_Role> UserRoles { get; set; } = new List<User_Role>();
    }
}
