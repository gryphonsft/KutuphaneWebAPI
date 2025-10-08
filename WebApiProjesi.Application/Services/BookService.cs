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
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }
        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }
        public async Task AddBookAsync(Book book)
        {
            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();
        }
        public async Task UpdateBookAsync(Book book)
        {
            await _bookRepository.UpdateAsync(book);
            await _bookRepository.SaveChangesAsync();
        }
        public async Task DeleteBookByIdAsync(int id)
        {
            await _bookRepository.GetByIdAsync(id);
            await _bookRepository.SaveChangesAsync();
        }
    }
}
