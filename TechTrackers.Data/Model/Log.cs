 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model
{
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Log_ID { get; set; }

        [ForeignKey("User")]
        public int Staff_ID { get; set; }
        public User? Staff { get; set; }

        [ForeignKey("Category")]
        public int Category_ID { get; set; }

        public Category? Category { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Attachment_URL { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public DateTime Created_At { get; set; }

        [ForeignKey("User")]
        public int Assigned_By { get; set; }
        public User? AssignedByUser { get; set; }

        public string Assigned_At { get; set; } = string.Empty;
        public DateTime Due_Date { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public int Technician_ID { get; set; }
        public User? Technician { get; set; }
        public string Log_Status {  get; set; } = string.Empty;
        public DateTime Updated_At { get; set; }

        [ForeignKey("SLA")]
        public int SLA_ID { get; set; }
        public SLA? SLA { get; set; }
        public virtual ICollection<Log_chat> LogChats { get; set; } = new List<Log_chat>();
        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        public virtual ICollection<Log_status_history> LogStatusHistories { get; set; } = new List<Log_status_history>();
    }
}
