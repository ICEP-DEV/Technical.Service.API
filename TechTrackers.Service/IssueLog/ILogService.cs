using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTrackers.Data.Model;

namespace TechTrackers.Service.IssueLog
{
    public interface ILogService
    {
        Task<Log> LogIssue(Log log);
        IEnumerable<Log> GetAllLogs();
        Task<Log> GetLogById(int id);
        Task<Log> UpdateLog(Log log);
        Task<bool> DeleteLog(int id);

        //Additional Methods
        IEnumerable<Log> GetLogsByStatus(string status);
        IEnumerable<Log> GetLogsByDateRange(DateTime startDate, DateTime endDate);
        int GetLogCount();
        Task<bool> AssignLogToTechnician(int logId, int technicianId);

        IEnumerable<Log> SearchLogs(string keyword);

/*        Task<int> ArchiveOldLogs(int days);
*/    
    
    }
}
