using Microsoft.AspNetCore.Identity;
using WebApiProjesi.Application.DTOs.Requests;
using WebApiProjesi.Application.DTOs.Respones;
using WebApiProjesi.Application.Interfaces;
using WebApiProjesi.Domain.Entities;
using WebApiProjesi.Domain.Interfaces;
using WebApiProjesi.Domain.User;

namespace WebApiProjesi.Application.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IBorrowRepository _borrowRepository;
        private readonly ILogRepository _logRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IBookCopyRepository _bookCopyRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public BorrowService(IBorrowRepository borrowRepository,
            ILogRepository logRepository, IBookRepository bookRepository,
            IBookCopyRepository bookCopyRepository,
            UserManager<AppUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _borrowRepository = borrowRepository;
            _logRepository = logRepository;
            _bookRepository = bookRepository;
            _bookCopyRepository = bookCopyRepository;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
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

        //GetBorrowByIdAsync eklenecek

        //En sona log için servis başvurusu.
        public async Task<BorrowResponseDto> CreateBorrowAsync(CreateBorrowRequest request)
        {
            // Kitap kontrolu icin
            var copyBook = await _bookCopyRepository.GetByIdAsync(request.CopyBookId);

            if (copyBook == null)
            {
                throw new Exception("Kitap verisi bulunamadi");
            }


            if (copyBook.Status != BookStatus.Musait)
            {
                throw new Exception("Kitap suan musait degil");
            }
            //Kullanici kontrolu icin
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
            {
                throw new Exception($"ID'si {request.UserId} olan kullanici bulunamadi");
            }

            var borrow = new Borrow
            {
                Id = Guid.NewGuid(),
                BookCopyId = request.CopyBookId,
                UserId = request.UserId,
                BorrowDate = DateTime.Now,
                ReturnDate = null
            };

            await _borrowRepository.AddAsync(borrow);

            copyBook.Status = BookStatus.Oduncte;
            await _bookCopyRepository.Update(copyBook);

            await _unitOfWork.SaveChangesAsync();

            var response = new BorrowResponseDto
            {
                BorrowId = borrow.Id,
                ISBN = copyBook.Book.ISBN,
                BookTitle = copyBook.Book.Title,
                AuthorName = copyBook.Book.AuthorName,
                CopyBookId = copyBook.Id,
                Username = user.UserName ?? string.Empty,
                FullName = user.FullName,
                BorrowDate = borrow.BorrowDate,
                ReturnDate = borrow.ReturnDate
            };
            return response;
        }

        // Kullanıcı bilgisine göre search metodu yazılacak.
        // Hedef olarak bir kitap belirlenip, onu kimlerin ödünç aldığını listeleyen metod yazılacak.
        //
    }
}