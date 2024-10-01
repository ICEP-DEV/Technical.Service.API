using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model
{
    public class Log
    {
        [Key]
        public int Log_ID { get; set; }
        [ForeignKey("User")]
        public int Staff_ID { get; set; }
        [ForeignKey("Category")]
        public int Category_ID { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Attachment_URL { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public DateTime Created_At { get; set; }

        [ForeignKey("User")]
        public int Assigned_By { get; set; }
        public DateTime Assigned_At { get; set; }
        public DateTime Due_Date { get; set; }
        [ForeignKey("User")]
        public int Technician_ID { get; set; }
        public string Log_Status {  get; set; } = string.Empty;
        public DateTime Updated_At { get; set; }
        [ForeignKey("Service_level_agreement")]
        public int SLA_ID { get; set; }
    }
}
