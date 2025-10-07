using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiProjesi.Application.DTOs.Requests
{
    public class CreateBookRequest
    {
        [Required(ErrorMessage = "Kitap başlığı zorunludur.")]
        [StringLength(250, ErrorMessage = "Başlık en fazla 250 karakter olabilir.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{10}(\d{3})?$", ErrorMessage = "Geçersiz ISBN formatı. (10 veya 13 haneli olmalı)")]
        public string ISBN { get; set; } = string.Empty;

        [Required]
        [Range(10, 5000, ErrorMessage = "Sayfa sayısı 10 ile 5000 arasında olmalıdır.")]
        public int PageCount { get; set; }

        [Required]
        public string AuthorName { get; set; } = string.Empty;
    }
}
