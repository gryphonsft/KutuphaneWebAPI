using WebApiProjesi.Application.DTOs.Respones;
using WebApiProjesi.Application.Interfaces;
using WebApiProjesi.Domain.Interfaces;

namespace WebApiProjesi.Application.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IBorrowRepository _borrowRepository;
        private readonly ILogRepository _logRepository;

        public BorrowService(IBorrowRepository borrowRepository,ILogRepository logRepository)
        {
            _borrowRepository = borrowRepository;
            _logRepository = logRepository;
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
        
    }
}