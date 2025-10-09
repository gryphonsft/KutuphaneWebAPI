using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Application.DTOs.Respones;
using WebApiProjesi.Domain.Entities;

namespace WebApiProjesi.Application.Interfaces
{
    public interface IBookService
    {
        //Regionlamalısın
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<BookResponseDto?> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task<IEnumerable<BookResponseDto>> SearchBooksAsync(string keyvalue);
        Task DeleteBookByIdAsync(int id);
    }
}
