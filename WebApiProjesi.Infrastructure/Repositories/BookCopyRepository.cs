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
    public class BookCopyRepository : IBookCopyRepository
    {
        private readonly ApplicationDbContext _context;

        public BookCopyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookCopy?> GetByIdAsync(Guid id) => await _context.BookCopy.FindAsync(id);
        public async Task Update(BookCopy bookCopy)
        {
             _context.Update(bookCopy);
        }
    }
}
