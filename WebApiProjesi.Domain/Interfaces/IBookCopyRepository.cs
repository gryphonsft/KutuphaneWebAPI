using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Domain.Entities;

namespace WebApiProjesi.Domain.Interfaces
{
    public interface IBookCopyRepository
    {
        Task<BookCopy?> GetByIdAsync(Guid id);
    }
}
