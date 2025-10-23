using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Domain.Abstractions;

namespace WebApiProjesi.Domain.Entities
{
    public sealed class Book : Entity
    {
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int PageCount { get; set; }
        public string AuthorName { get; set; } = string.Empty;

        public ICollection<Borrow> Borrows { get; set; }
    }
}
