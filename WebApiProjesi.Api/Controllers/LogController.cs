using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiProjesi.Application.Interfaces;
using WebApiProjesi.Domain.Entities;

namespace WebApiProjesi.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        #region Read servisi
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var logs = await _logService.GetAllLogsAsync();
            return Ok(logs);
        }
        #endregion

        #region Queries servisi
        [HttpGet("level/{level}")]
        public async Task<IActionResult> GetByLevel(AppLogLevel level)
        {
            var logs = await _logService.GetLogsByLevelAsync(level);
            return Ok(logs);
        }
  
        [HttpGet("Search")]
        public async Task<IActionResult> Search (string keyvalue)
        {
            var result = await _logService.SearchLogsAsync(keyvalue);

            if (!result.Any())
                return NotFound(new { message = "Eşleşen log kaydı bulunamadı." });

            return Ok(result);
        }
        #endregion
    }
}
