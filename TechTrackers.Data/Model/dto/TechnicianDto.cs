using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model.dto
{
    public class TechnicianDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Specialization { get; set; }
        public string? Contact { get; set; }
        public string? Password { get; set; } // Ideally, hash this before storin
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}
