using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Domain.Entities;
using WebApiProjesi.Domain.Interfaces;
using WebApiProjesi.Infrastructure.Data;

namespace WebApiProjesi.Infrastructure.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly ApplicationDbContext _context;

        public LogRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #region Log Operasyonları (Log ekle, Bütün logları getir, Log ara)

        //Log eklenmesi.
        public async Task AddAsync (Logs logs)
        {
            await _context.Logs.AddAsync(logs);
        }
        //Bütün logları getirir.
        public async Task<IEnumerable<Logs>> GetAllAsync()
        {
           return await _context.Logs.OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }
        //Logları aramak.
        public async Task <IEnumerable<Logs>> FindAsync(Expression<Func<Logs, bool>> predicate)
        {
            return await _context.Logs
                .Where(predicate)
                .ToListAsync();
        }

        //Loglar arasında aramak yapmak için
        public async Task<IEnumerable<Logs>> SearchAsync(string keyvalue)
        {
            keyvalue = keyvalue.ToLower();

            return await _context.Logs
                .Where(l =>
                    l.Action.ToLower().Contains(keyvalue) ||
                    l.Message.ToLower().Contains(keyvalue) ||
                    l.Level.ToString().ToLower().Contains(keyvalue) ||
                    l.CreatedAt.ToString().Contains(keyvalue))
                .ToListAsync();
        }


        //Değişiklikleri kaydeder.
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
