using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Domain.Entities;

namespace WebApiProjesi.Domain.Interfaces
{
    public interface IBorrowRepository
    {
        Task<IEnumerable<Borrow>> GetAllAsync();
        Task<Borrow?> GetByIdAsync(Guid id);
        Task AddAsync(Borrow borrow);
        Task Update(Borrow borrow);
        Task DeleteByIdAsync(Guid id);
        Task<IEnumerable<Borrow>> FindAsync(Expression<Func<Borrow, bool>> predicate);
    }
}
