using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiProjesi.Application.DTOs.Requests
{
    public sealed record CreateBookCopiesRequest(Guid bookId,int numberOfCopies);
}
