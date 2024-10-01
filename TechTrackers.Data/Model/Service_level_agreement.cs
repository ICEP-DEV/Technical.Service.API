using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model
{
    public class Service_level_agreement
    {
        [Key]
        public int SLA_ID { get; set; }
        //still not finalized(Check with group)
        [ForeignKey("Log")]
        public int Log_ID { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Resolution_Timeframe { get; set; }
        public string Priority_Level { get; set; } = string.Empty;
    }
}
