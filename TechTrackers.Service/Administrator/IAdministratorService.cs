using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTrackers.Data.Model;
using TechTrackers.Data.Model.dto;

namespace TechTrackers.Service.Administrator
{
    public interface IAdministratorService
    {
        Task<SLA> AddSLA(string priority);
        Task<RespondWrapper> AssignSLAToLog(int slaId, int logId);
        Task<RespondWrapper> CheckAndHandleSLACompliance(int logId);
    }
}
