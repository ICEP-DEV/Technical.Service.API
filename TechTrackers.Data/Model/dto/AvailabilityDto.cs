using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model.dto
{
    public class AvailabilityDto
    {
        public int TechnicianID { get; set; }
        public required string Specialization { get; set; }
        public required string Contact { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}
