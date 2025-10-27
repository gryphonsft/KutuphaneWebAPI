using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Domain.Entities;

namespace WebApiProjesi.Application.DTOs.Respones
{
    public class BorrowResponseDto
    {
        public Guid BorrowId { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string BookTitle { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;

        public Guid CopyBookId { get; set; }
        public BookStatus BookCopyStatus { get; set; }

        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

    }

}
