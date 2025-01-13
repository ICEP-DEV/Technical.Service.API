﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model.dto
{
    public class TechnicianCreateDTO
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Specialization { get; set; }
        public string? Contact { get; set; }
        public string? Password { get; set; }
        public string? FromTime { get; set; }
        public string? ToTime { get; set; }
    }
}
