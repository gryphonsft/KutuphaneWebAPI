using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Application.DTOs.Requests;
using WebApiProjesi.Application.DTOs.Respones;
using WebApiProjesi.Domain.Entities;

namespace WebApiProjesi.Application.Interfaces
{
    public interface IBookService
    {
        #region Read servisi
        Task<IEnumerable<BookResponseDto>> GetAllBooksAsync();
        Task<BookResponseDto?> GetBookByIdAsync(int id);
        #endregion

        #region Create servisi
        Task AddBookAsync(CreateBookRequest bookRequest);
        #endregion

        #region Update servisi
        Task<bool> UpdateBookAsync(int id, BookResponseDto dto);
        #endregion

        #region Delete servisi
        Task DeleteBookByIdAsync(int id);
        #endregion

        #region Queries servisi
        Task<IEnumerable<BookResponseDto>> SearchBooksAsync(string keyvalue);
        #endregion
    }
}
