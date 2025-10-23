using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiProjesi.Application.DTOs.Requests;
using WebApiProjesi.Application.DTOs.Respones;
using WebApiProjesi.Application.Interfaces;
using WebApiProjesi.Domain.Entities;

namespace WebApiProjesi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        //Bi' ara AutoMapper eklerim.
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _bookService.GetAllBooksAsync();

            if (response == null)
                return NotFound();

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