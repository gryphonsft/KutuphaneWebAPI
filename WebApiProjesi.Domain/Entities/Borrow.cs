using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Domain.User;

namespace WebApiProjesi.Domain.Entities
{
    public sealed class Borrow
    {
        public Guid Id { get; set; }

        public Guid BookCopyId { get; set; }
        public BookCopy BookCopy { get; set; }

        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        // Eğer ReturnDate bir değere sahipse true, değilse false
        public bool IsReturned => ReturnDate.HasValue; 

    }
}
