using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model.dto
{
    public class LogDto
    {
        public string? Description { get; set; }
        public string? Priority { get; set; }
        public DateTime? Assigned_at { get; set; }
        public string? Log_Status { get; set; }
        public int Technician_ID { get; set; }
        public int Staff_ID { get; set; }
        public int SLA_ID { get; set; }
        public int Category_ID { get; set; }
       /*public string? Issue_Title { get; set; }
        public int Category_ID { get; set; }
        public string? Department { get; set; }
        public string? Priority { get; set; }
        public string? Description { get; set; }
        public DateTime Created_at { get; set; }
        public string? Assigned_at { get; set; }
        public string? AttechmentUrl { get; set; }
        public int Staff_ID { get; set; }*/

    }
}
