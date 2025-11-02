using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Domain.Entities;

namespace WebApiProjesi.Domain.Interfaces
{
    public interface IBookCopyRepository
    {
        Task AddRangeAsync(IEnumerable<BookCopy> bookCopies);
        Task<BookCopy?> GetByIdAsync(Guid id);
        Task<int> CountAsync(Expression<Func<BookCopy, bool>> predicate);
        Task Update(BookCopy bookCopy);
        Task SaveChangesAsync();
    }
}
