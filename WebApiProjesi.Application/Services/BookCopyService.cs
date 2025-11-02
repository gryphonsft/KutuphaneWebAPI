using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Application.Interfaces;
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
        public async Task AddBookCopiesAsync(Guid bookId, Guid numberOfCopies)
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
        }
    }
}
