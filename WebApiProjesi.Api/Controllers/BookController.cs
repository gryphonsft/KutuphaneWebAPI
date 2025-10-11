using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiProjesi.Application.DTOs.Requests;
using WebApiProjesi.Application.DTOs.Respones;
using WebApiProjesi.Application.Interfaces;
using WebApiProjesi.Domain.Entities;

namespace WebApiProjesi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        //Bi' ara AutoMapper eklerim. Bu proje için çok gelir.
        //Regionlamalısın
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllBooksAsync();

            var response = books.Select(b => new BookResponseDto
            {
                Title = b.Title,
                ISBN = b.ISBN,
                PageCount = b.PageCount,
                AuthorName = b.AuthorName,
            }).ToList();

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var books = await _bookService.GetBookByIdAsync(id);

            if (books == null)
                return NotFound();

            return Ok(books);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookRequest dto)
        {
            await _bookService.AddBookAsync(dto);
            return Ok("Kitap başarıyla eklendi.");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookResponseDto dto)
        {
            var request = await _bookService.UpdateBookAsync(id, dto);

            if (!request)
                return NotFound();
            return Ok("Güncelleme başarılı");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBookByIdAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string keyvalue)
        {
            var result = await _bookService.SearchBooksAsync(keyvalue);
            return Ok(result);
        }
    }
}