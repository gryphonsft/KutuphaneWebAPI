using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiProjesi.Application.DTOs.Requests;
using WebApiProjesi.Application.Interfaces;

namespace WebApiProjesi.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowService _borrowService;

        public BorrowController(IBorrowService borrowService)
        {
            _borrowService = borrowService;
        }
        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            var response = await _borrowService.GetAllBorrowAsync();

            if (response is null)
                return NotFound();

            return Ok(response);
        }
        [HttpPost]
        public async Task <IActionResult> Create([FromBody] CreateBorrowRequest dto)
        {
            await _borrowService.CreateBorrowAsync(dto);
            return Ok("Başarıyla ödünç alındı");
        }
    }
}
