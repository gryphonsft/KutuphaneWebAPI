using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Application.DTOs.Respones;
using WebApiProjesi.Domain.Entities;

namespace WebApiProjesi.Application.Interfaces
{
    public interface IBookCopyService
    {
        Task<IEnumerable<BookCopy>> GetAllBookCopyAsync();
        Task<IEnumerable<BookCopyResponseDto>> GetAllBookCopyDetailsAsync();
        Task AddBookCopiesAsync(Guid bookId, int numberOfCopies);
    }
}
