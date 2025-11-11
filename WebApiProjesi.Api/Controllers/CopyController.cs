using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiProjesi.Application.DTOs.Requests;
using WebApiProjesi.Application.Interfaces;
using WebApiProjesi.Application.Services;

namespace WebApiProjesi.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CopyController : ControllerBase
    {
        private readonly IBookCopyService _bookCopyService;

        public CopyController(IBookCopyService bookCopyService)
        {
            _bookCopyService = bookCopyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _bookCopyService.GetAllBookCopyAsync();

            if (response == null)
                return NotFound();

            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDetails()
        {
            var response = await _bookCopyService.GetAllBookCopyDetailsAsync();

            if (response == null)
                return NotFound();

            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateBookCopiesRequest request)
        {
            await _bookCopyService.AddBookCopiesAsync(request);
            return Ok("Kitabın kopyası başarıyla eklendi");
        }
    }
}
