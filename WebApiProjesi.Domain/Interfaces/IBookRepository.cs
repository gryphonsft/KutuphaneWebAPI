using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Domain.Entities;

namespace WebApiProjesi.Domain.Interfaces
{
    public interface IBookRepository
    {
        //Regionlamalısın
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(Guid id);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteByIdAsync(Guid id);
        Task<IEnumerable<Book>> FindAsync(Expression<Func<Book, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<Book, bool>> predicate);
        Task SaveChangesAsync();
    }
}
