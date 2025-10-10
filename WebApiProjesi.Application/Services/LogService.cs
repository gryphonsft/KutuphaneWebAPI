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
        public async Task AddLogsAsync(string action, string message, AppLogLevel level = AppLogLevel.Info)
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
        public async Task<IEnumerable<Logs>> GetAllLogsAsync()
        {
            return await _logRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Logs>> GetLogsByLevelAsync (AppLogLevel level)
        {
            return await _logRepository.FindAsync(l => l.Level == level);
        }
    }
}
