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
        public int SLA_ID { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Resolution_Timeframe { get; set; }
        public string Priority_Level { get; set; } = string.Empty;

        public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
    }
}
