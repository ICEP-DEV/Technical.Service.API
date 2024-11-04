using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model.dto
{
    public class SLAComplianceDto
    {
        public int LogId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ResolutionTimeframe { get; set; } // In hours
    }
}
