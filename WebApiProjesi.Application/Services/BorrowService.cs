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
        
    }
}