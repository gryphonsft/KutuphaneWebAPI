using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Domain.Entities;

namespace WebApiProjesi.Domain.Interfaces
{
    public interface ILogRepository
    {
        //Regionla bi ara burayı.

        Task AddAsync(Logs logs);
        Task<IEnumerable<Logs>> GetAllAsync();
        Task<IEnumerable<Logs>> FindAsync(Expression <Func<Logs, bool>> predicate);
        Task<IEnumerable<Logs>> SearchAsync(string keyvalue);
        Task SaveChangesAsync();
    }
}
