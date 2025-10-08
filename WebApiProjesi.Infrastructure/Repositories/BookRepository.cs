using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Book>> GetAllAsync() => await _context.Book.ToListAsync();
        public async Task<Book?> GetByIdAsync(int id) => await _context.Book.FindAsync(id);
        public async Task AddAsync(Book book)
        {
            await _context.Book.AddAsync(book);
        }
        public async Task UpdateAsync(Book book)
        {
            _context.Book.Update(book);
        }
        public async Task DeleteByIdAsync(int id)
        {
            var book = await GetByIdAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
