using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model
{
    public class Technician
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TechnicianId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }
        public string? Specialization { get; set; }
        public string? Contact { get; set; }
        public TimeSpan? FromTime { get; set; } // Start of availability
        public TimeSpan? ToTime { get; set; }
    }
}
