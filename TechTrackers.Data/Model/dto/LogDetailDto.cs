﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model.dto
{
    public class LogDetailDto
    {
        public string? IssueId { get; set; } // Combine department name and log ID
        public string? AssignedTo { get; set; }
        public string? IssueTitle { get; set; }
        public string? Location { get; set; }
        public string? CategoryName { get; set; }
        public string? IssuedAt { get; set; }
        public string? Department { get; set; }
        public string? Priority { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public string? AttachmentBase64 { get; set; }
    }
}
