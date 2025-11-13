using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApiProjesi.Application.DTOs.Requests;
using WebApiProjesi.Application.DTOs.Respones;
using WebApiProjesi.Application.Interfaces;
using WebApiProjesi.Domain.Entities;
using WebApiProjesi.Domain.User;

namespace WebApiProjesi.Application.Services
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogService _logService;

        public AuthService(UserManager<AppUser> userManager, ILogService logService)
        {
            _userManager = userManager;
            _logService = logService;
        }
        #region User authentication servisi
        public async Task<(bool Success,string Message, Guid? UserId)> LoginAsync(LoginUserDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username);

            if (user == null)
            {
                return (false,"Böyle bir kullanıcı bulunamadı",null);
            }
            var result = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!result)
            {
                return (false,"Kullanıcı adı yada şifre hatalı", null);
            }

            await _logService.AddLogsAsync("Kullanıcı girişi", $"{user.FullName} adlı kullanıcı giriş yaptı.",AppLogLevel.Bilgi);

            return (true, "Giris basarili", user.Id);
        }

        public async Task<(bool Success, string Message)> RegisterAsync(RegisterUserDto dto)
        {
            var existingUser = await _userManager.FindByNameAsync(dto.Username);
            if (existingUser != null)
                return (false, "Bu kullanıcı adı zaten alınmış");

            var user = new AppUser
            {
                UserName = dto.Username,
                Email = dto.Email,
                FullName = dto.FullName,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return (false, errors);
            }

            await _logService.AddLogsAsync("Kullanıcı kaydı", $"{user.FullName} adıyla sisteme kaydoldu.",AppLogLevel.Bilgi);

            return (true, "Kullanıcı başarıyla oluşturuldu.");
        }
        #endregion

        #region Read servisi
        public async Task<List<UserDto>> GetAllUserAsync()
        {
            return await _userManager.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Email = u.Email,
                    Fullname = u.FullName
                })
                .ToListAsync();
        }
        #endregion
    }
}
