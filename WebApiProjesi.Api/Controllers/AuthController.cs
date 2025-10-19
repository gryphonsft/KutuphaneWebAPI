using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiProjesi.Application.DTOs.Requests;
using WebApiProjesi.Domain.User;

namespace WebApiProjesi.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]CreateUserDto dto) 
        {
            var user = new AppUser
            {
                UserName = dto.Username,
                Email = dto.Email,
                FullName = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Kullanıcı başarıyla oluşturuldu");

        }
    }
}
