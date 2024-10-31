using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model
{
    public class User_Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_Role_ID { get; set; }

        [ForeignKey("User")]
        public int User_ID { get; set; }
        public User? User { get; set; }

        [ForeignKey("Role")]
        public int Role_ID { get; set; }
        public Role? Role { get; set; }

    }
}
