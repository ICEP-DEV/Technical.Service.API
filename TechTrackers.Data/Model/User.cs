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
        public int User_ID { get; set; }
        public string Surname { get; set; } = string.Empty;
        public string Initials { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [ForeignKey("Department")]
        public int Department_ID { get; set; }
        [ForeignKey("Role")]
        public int Role_ID { get; set; }
    }
}
