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
    public class BookCopyService : IBookCopyService
    {
        private readonly IBookCopyRepository _bookCopyRepository;
        private readonly IBookRepository _bookRepository;

        public BookCopyService(IBookCopyRepository bookCopyRepository,IBookRepository bookRepository)
        {
            _bookCopyRepository = bookCopyRepository;
            _bookRepository = bookRepository;
        }
        public async Task<IEnumerable<BookCopy>> GetAllBookCopyAsync()
        {
           var response = await _bookCopyRepository.GetAllAsync();

           return response;
        }
        public async Task<IEnumerable<BookCopyResponseDto>> GetAllBookCopyDetailsAsync()
        {
            var bookCopies = await _bookCopyRepository.GetAllDetailsAsync();

            return bookCopies.Select(bc => new BookCopyResponseDto
            {
                Id = bc.Id,
                Status = bc.Status.ToString(),
                BookTitle = bc.Book.Title,
                Author = bc.Book.AuthorName,
                PageCount = bc.Book.PageCount
            });
        }
        public async Task AddBookCopiesAsync(Guid bookId, int numberOfCopies)
        {
            var bookExists = await _bookRepository.AnyAsync(x => x.Id == bookId); 

            if(!bookExists)
            {
                throw new Exception("Belirtilen kitap bulunamadi.");
            }

            var existingCopies = await _bookCopyRepository.CountAsync(x => x.BookId == bookId);

            if(existingCopies > 0)
            {
                throw new Exception("Bu kitap için zaten fiziksel kopyalar mevcut.");
            }

            var copies = new List<BookCopy>();
            for (int i = 0; i < numberOfCopies; i++)
            {
                copies.Add(new BookCopy
                {
                    BookId = bookId
                });
            }

            await _bookCopyRepository.AddRangeAsync(copies);
            await _bookCopyRepository.SaveChangesAsync();
        }
    }
}
