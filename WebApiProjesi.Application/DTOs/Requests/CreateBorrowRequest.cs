using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiProjesi.Application.DTOs.Requests
{
    public class CreateBorrowRequest
    {
        [Required(ErrorMessage ="CopyBookId doldurulmak zorundadır.")]
        public Guid CopyBookId { get; set; }

        [Required(ErrorMessage ="UserId doldurulmak zorundadır.")]
        public Guid UserId { get; set; }
    }
}
