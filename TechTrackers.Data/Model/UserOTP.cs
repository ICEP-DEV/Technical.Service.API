using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTrackers.Data.Model
{
    public class UserOtp
    {
        public int Id { get; set; }
        public string? UserEmail { get; set; }
        public string? OtpCode { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsValid { get; set; } = true;  // Defaults to true, then set to false once used
    }
}
