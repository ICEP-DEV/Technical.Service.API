using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model
{
    public class Notifications
    {
        private DateTime timestamp;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Notification_ID { get; set; }

        [ForeignKey("Log")]
        public int Log_ID { get; set; }
        [ForeignKey("User")]
        public int User_ID { get; set; }
        public int Notification_Type { get; set; }
        public string Notification_Message { get; set; } = string.Empty;
        public DateTime Timestamp { get => timestamp; set => timestamp = value; }
        public bool Read_Status { get; set; }
    }
}
