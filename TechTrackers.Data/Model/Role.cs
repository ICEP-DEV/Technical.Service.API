using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model
{
    public class Role
    {
        [Key]
        public int Role_ID { get; set; }
        //to be discussed
        public string Role_Name { get; set; } = string.Empty;
        public string Description { get; set;} = string.Empty;
    }
}
