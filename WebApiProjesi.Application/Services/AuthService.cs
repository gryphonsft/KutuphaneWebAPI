using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApiProjesi.Application.DTOs.Requests;
using WebApiProjesi.Application.DTOs.Respones;
using WebApiProjesi.Application.Interfaces;
using WebApiProjesi.Domain.User;

namespace WebApiProjesi.Application.Services
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<AppUser> _userManager;

        public AuthService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
                return (false, "Kullanıcı adı yada şifre hatalı", null);
            }

            return (true, "Giris basarili", user.Id);
        }

        public async Task<(bool Success, string Message)> RegisterAsync(RegisterUserDto dto)
        {
            var existingUser = _userManager.FindByNameAsync(dto.Username);
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
