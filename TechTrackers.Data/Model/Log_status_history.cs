using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model
{
    public class Log_status_history
    {
        [Key]
        public int Log_Status_History_ID { get; set; }
        [ForeignKey("Log")]
        public int Log_ID { get; set; }
        [ForeignKey("User")]
        public int Changed_by_User_ID { get; set; }
        public string Log_Status { get; set; } = string.Empty;
        public DateTime Updated_At { get; set; }
    }
}
