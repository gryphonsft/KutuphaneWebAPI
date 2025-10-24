using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Application.Interfaces;
using WebApiProjesi.Domain.Entities;
using WebApiProjesi.Domain.Interfaces;

namespace WebApiProjesi.Application.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }
        #region Create servisi
        public async Task AddLogsAsync(string action, string message, AppLogLevel level = AppLogLevel.Bilgi)
        {
            var log = new Logs
            {
                Action = action,
                Message = message,
                Level = level,
                CreatedAt = DateTime.UtcNow
            };

            await _logRepository.AddAsync(log);
            await _logRepository.SaveChangesAsync();
        }
        #endregion

        #region Read servisi
        public async Task<IEnumerable<Logs>> GetAllLogsAsync()
        {
            return await _logRepository.GetAllAsync();
        }
        #endregion

        #region Queries servisi
        public async Task<IEnumerable<Logs>> GetLogsByLevelAsync (AppLogLevel level)
        {
            return await _logRepository.FindAsync(l => l.Level == level);
        }

        public async Task<IEnumerable<Logs>> SearchLogsAsync(string keyvalue)
        {
            if (string.IsNullOrWhiteSpace(keyvalue) || keyvalue.Length < 3)
                return Enumerable.Empty<Logs>();

            var logs = await _logRepository.SearchAsync(keyvalue);

            return logs.OrderByDescending(l => l.CreatedAt);
        }
        #endregion
    }
}
