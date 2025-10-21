using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Application.DTOs.Requests;
using WebApiProjesi.Application.DTOs.Respones;
using WebApiProjesi.Application.Interfaces;
using WebApiProjesi.Domain.Entities;
using WebApiProjesi.Domain.Interfaces;

namespace WebApiProjesi.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogService _logService;


        public BookService(IBookRepository bookRepository, ILogService loogService)
        {
            _bookRepository = bookRepository;
            _logService = loogService;

        }

        #region Crud Operasyonlari
        
        //Asenkron (async) çalışan temel CRUD işlemleri
        public async Task<IEnumerable<BookResponseDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();

            var response = books.Select(b => new BookResponseDto
            {
                Title = b.Title,
                ISBN = b.ISBN,
                PageCount = b.PageCount,
                AuthorName = b.AuthorName,
            }).ToList();

            return response;
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
        public async Task AddBookAsync(CreateBookRequest bookRequest)
        {
            var book = new Book
            {
                Title = bookRequest.Title,
                ISBN = bookRequest.ISBN,
                PageCount = bookRequest.PageCount,
                AuthorName = bookRequest.AuthorName,
            };

            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();

            await _logService.AddLogsAsync("AddBook", $"Yeni kitap eklendi: {book.Title}", AppLogLevel.Info);
        }
        public async Task<bool> UpdateBookAsync(int id, BookResponseDto dto)
        {
            var exisBook = await _bookRepository.GetByIdAsync(id);

            if (exisBook == null)
                return false;

            exisBook.Title = dto.Title;
            exisBook.ISBN = dto.ISBN;
            exisBook.PageCount = dto.PageCount;
            exisBook.AuthorName = dto.AuthorName;

            await _bookRepository.UpdateAsync(exisBook);
            await _bookRepository.SaveChangesAsync();

            return true;
        }
        public async Task DeleteBookByIdAsync(int id)
        {

            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                throw new Exception("Kitap bulunamadı.");


            await _bookRepository.DeleteByIdAsync(id);
            await _bookRepository.SaveChangesAsync();


            await _logService.AddLogsAsync("RemoveBook", $"Kitap silindi: {book.Title}", AppLogLevel.Info);
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
