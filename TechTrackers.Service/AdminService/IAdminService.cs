using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTrackers.Data.Model;
using TechTrackers.Data.Model.dto;

namespace TechTrackers.Service.AdminService
{
    public interface IAdminService
    {
        Task<SLA> AddSLA(string priority);
        Task<bool> AssignSLAToLog(int slaId, int logId);
        Task<bool> CheckAndHandleSLACompliance(SLAComplianceDto complianceDto);
    }
}
