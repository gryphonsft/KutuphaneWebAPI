using WebApiProjesi.Application.DTOs.Requests;
using WebApiProjesi.Application.DTOs.Respones;
using WebApiProjesi.Application.Services;

namespace WebApiProjesi.Application.Interfaces
{
    public interface IBorrowService
    {
        Task<IEnumerable<BorrowResponseDto>> GetAllBorrowAsync();
        Task<BorrowResponseDto> CreateBorrowAsync(CreateBorrowRequest request);
    }
}