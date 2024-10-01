using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model
{
    public class Feedback
    {
        [Key]
        public int Feedback_ID { get; set; }
        [ForeignKey("Log")]
        public int Log_ID { get; set; }
        [ForeignKey("User")]
        public int User_ID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }


    }
}
