using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Domain.Abstractions;

namespace WebApiProjesi.Domain.Entities
{
    // Kitapların fiziksel nüshalarının birden fazla olma durumundan dolayı, kitaplar buradan copy edilip borrow'a gitmeli.
    public sealed class BookCopy : Entity
    {
        
        public Guid BookId { get; set; }
        public Book Book { get; set; }

        public BookStatus Status { get; set; }

        public ICollection<Borrow> Borrows { get; set; }  
    }
    public enum BookStatus
    {
        Musait,
        Oduncte,
        Kayip,
        Hasarli
    }
}
