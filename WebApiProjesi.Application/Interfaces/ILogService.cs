using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Domain.Entities;

namespace WebApiProjesi.Application.Interfaces
{
    public interface ILogService
    {
        Task AddLogsAsync(string action, string message, AppLogLevel level = AppLogLevel.Info);
        Task<IEnumerable<Logs>> GetAllLogsAsync();
        Task<IEnumerable<Logs>> GetLogsByLevelAsync(AppLogLevel level);
    }
}
