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
        #region Create servisi
        Task AddLogsAsync(string action, string message, AppLogLevel level = AppLogLevel.Bilgi);
        #endregion

        #region Read servisi
        Task<IEnumerable<Logs>> GetAllLogsAsync();
        #endregion

        #region Queries servisi
        Task<IEnumerable<Logs>> GetLogsByLevelAsync(AppLogLevel level);
        Task<IEnumerable<Logs>> SearchLogsAsync(string keyvalue);
        #endregion
    }
}
