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
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Regionlamalısın
        public async Task<IEnumerable<Book>> GetAllAsync() => await _context.Book.ToListAsync();
        public async Task<Book?> GetByIdAsync(Guid id)
        {
            return await _context.Book
                .Include(b => b.BookCopies)  
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task AddAsync(Book book)
        {
            await _context.Book.AddAsync(book);
        }
        public async Task UpdateAsync(Book book)
        {
            _context.Book.Update(book);
        }
        public async Task DeleteByIdAsync(Guid id)
        {
            var book = await GetByIdAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }
        }
        public async Task <IEnumerable<Book>> FindAsync(Expression<Func<Book, bool>> predicate)
        {
            return await _context.Book
                .Where(predicate)
                .ToListAsync();
        }
        public async Task<bool> AnyAsync(Expression<Func<Book, bool>> predicate)
        {
            return await _context.Book.AnyAsync(predicate);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
