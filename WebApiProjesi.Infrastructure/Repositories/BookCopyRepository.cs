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
    public class BookCopyRepository : IBookCopyRepository
    {
        private readonly ApplicationDbContext _context;

        public BookCopyRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(BookCopy bookCopy)
        {
           await _context.BookCopies.AddAsync(bookCopy);
        }
        public async Task AddRangeAsync(IEnumerable<BookCopy> bookCopies)
        {
            await _context.BookCopies.AddRangeAsync(bookCopies);
        }
        public async Task<BookCopy?> GetByIdAsync(Guid id) => await _context.BookCopies.FindAsync(id);
        public async Task<int> CountAsync(Expression<Func<BookCopy, bool>> predicate)
        {
            return await _context.BookCopies.CountAsync(predicate);
        }
        public async Task Update(BookCopy bookCopy)
        {
             _context.Update(bookCopy);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
