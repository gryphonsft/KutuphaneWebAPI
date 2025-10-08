using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiProjesi.Application.DTOs.Requests;
using WebApiProjesi.Application.Interfaces;
using WebApiProjesi.Domain.Entities;

namespace WebApiProjesi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
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
            var book = new Book
            {
                Title = dto.Title,
                ISBN = dto.ISBN,
                PageCount = dto.PageCount,
                AuthorName = dto.AuthorName,
                CreatedDate = DateTime.UtcNow
            };
            await _bookService.AddBookAsync(book);
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Book book)
        {
            if (id != book.Id)
                return BadRequest();
            await _bookService.UpdateBookAsync(book);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBookByIdAsync(id);
            return NoContent();

        }
    }
}