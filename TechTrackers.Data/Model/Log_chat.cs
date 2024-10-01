using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model
{
    public class Log_chat
    {
        [Key]
        public int Log_Chat_ID { get; set; }
        [ForeignKey("Log")]
        public int Log_ID { get; set; }
        [ForeignKey("User")]
        public int Sender_ID { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }

    }
}
