using WebApiProjesi.Application.DTOs.Requests;
using WebApiProjesi.Application.DTOs.Respones;
using WebApiProjesi.Application.Interfaces;
using WebApiProjesi.Domain.Interfaces;

namespace WebApiProjesi.Application.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IBorrowRepository _borrowRepository;
        private readonly ILogRepository _logRepository;
        private readonly IBookRepository _bookRepository;

        public BorrowService(IBorrowRepository borrowRepository,ILogRepository logRepository,IBookRepository bookRepository)
        {
            _borrowRepository = borrowRepository;
            _logRepository = logRepository;
            _bookRepository = bookRepository;
        }
        public async Task<IEnumerable<BorrowResponseDto>> GetAllBorrowAsync()
        {
            var borrow = await _borrowRepository.GetAllAsync();

            var result = borrow.Select(b => new BorrowResponseDto
            {
                BorrowId = b.Id,
                ISBN = b.BookCopy.Book.ISBN,
                BookTitle = b.BookCopy.Book.Title,
                AuthorName = b.BookCopy.Book.AuthorName,
                CopyBookId = b.BookCopy.Id,
                BookCopyStatus = b.BookCopy.Status,
                Username = b.AppUser.UserName ?? string.Empty,
                FullName = b.AppUser.FullName,
                BorrowDate = b.BorrowDate,
                ReturnDate = b.ReturnDate
            }).ToList();

            return result;
        }

        public async Task<CreateBorrowRequest> CreateBorrowAsync(CreateBorrowRequest request)
        {
            //Buraya BookRepository baðlý fakat BorrowService için BookCopyRepository ile veri alýþveriþi saðlanmalý.
            //Bir BookCopyRepository olustur ve GetByIdAsync methodunu buradan saðla.
            var copyBook = await _bookRepository.GetByIdAsync(request.CopyBookId);

            if (copyBook == null)
                throw new Exception("Kitap kopyasý bulunamadý");

          //  if(copyBook.Status)
        }
    }
}