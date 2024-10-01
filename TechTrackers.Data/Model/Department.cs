using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model
{
    public class Department
    {
        [Key]
        public int Department_ID { get; set; }
        public string Department_Name { get; set; } = string.Empty;
    }
}
