using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model
{
    public class SLA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SLAId { get; set; }

        public int LogId { get; set; }
        public Log? Log { get; set; }

        public string Description { get; set; } = string.Empty;

        public int ResolutionTimeframe { get; set; } // In hours

        [Required]
        public string? PriorityLevel { get; set; }
    }
}
