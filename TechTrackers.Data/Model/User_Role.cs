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
        public int User_Role_ID { get; set; }
        [ForeignKey("User")]
        public int User_ID { get; set; }
        [ForeignKey("Role")]
        public int Role_ID { get; set; }

    }
}
