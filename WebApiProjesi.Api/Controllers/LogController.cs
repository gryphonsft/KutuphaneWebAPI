using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiProjesi.Application.Interfaces;
using WebApiProjesi.Domain.Entities;

namespace WebApiProjesi.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var logs = await _logService.GetAllLogsAsync();
            return Ok(logs);
        }
        [HttpGet("level/{level}")]
        public async Task<IActionResult> GetByLevel(AppLogLevel level)
        {
            var logs = await _logService.GetLogsByLevelAsync(level);
            return Ok(logs);
        }
    }
}
