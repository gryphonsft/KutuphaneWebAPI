using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Application.DTOs.Respones;
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

        #region Crud Operasyonları
        //Asenkron (async) çalışan temel CRUD işlemleri
        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }
        public async Task<BookResponseDto?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                return null;
            return new BookResponseDto
                {
                    Title = book.Title,
                    ISBN = book.ISBN,
                    PageCount = book.PageCount,
                    AuthorName = book.AuthorName,
                };
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
        #endregion

        #region Arama Operasyonları

        public async Task<IEnumerable<BookResponseDto>> SearchBooksAsync(string keyvalue)
        {
            if (string.IsNullOrWhiteSpace(keyvalue) || keyvalue.Length <= 3)
                return Enumerable.Empty<BookResponseDto>();
          
            var books = await _bookRepository.FindAsync(b =>
           !b.IsDeleted &&
           (b.Title.ToLower().Contains(keyvalue) ||
            b.AuthorName.ToLower().Contains(keyvalue) ||
            b.ISBN.ToLower().Contains(keyvalue)));

            return books.Select(b => new BookResponseDto
            {
                Title = b.Title,
                AuthorName = b.AuthorName,
                ISBN = b.ISBN,
                PageCount = b.PageCount
            });
        }
        #endregion
    }
}
