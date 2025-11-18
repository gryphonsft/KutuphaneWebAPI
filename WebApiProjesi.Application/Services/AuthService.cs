using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiProjesi.Application.DTOs.Requests;
using WebApiProjesi.Application.DTOs.Respones;
using WebApiProjesi.Application.Interfaces;
using WebApiProjesi.Domain.Entities;
using WebApiProjesi.Domain.User;

namespace WebApiProjesi.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogService _logService;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<AppUser> userManager, ILogService logService, IConfiguration configuration)
        {
            _userManager = userManager;
            _logService = logService;
            _configuration = configuration;
        }
        #region User authentication servisi
        public async Task<(bool Success, string Message, Guid? UserId, string? Token)> LoginAsync(LoginUserDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username);

            if (user == null)
                return (false, "Böyle bir kullanıcı bulunamadı", null, null);

            var result = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!result)
                return (false, "Kullanıcı adı ya da şifre hatalı", null, null);

            await _logService.AddLogsAsync("Kullanıcı girişi",
                $"{user.FullName} adlı kullanıcı giriş yaptı.",
                AppLogLevel.Bilgi);

            var token = GenerateJwtToken(user);

            return (true, "Giriş başarılı", user.Id, token);
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

            await _logService.AddLogsAsync("Kullanıcı kaydı", $"{user.FullName} adıyla sisteme kaydoldu.", AppLogLevel.Bilgi);

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
                    Username = u.UserName ?? string.Empty,
                    Email = u.Email ?? string.Empty,
                    Fullname = u.FullName
                })
                .ToListAsync();
        }
        #endregion

        #region Jwt oluşturma Servisi

        private string GenerateJwtToken(AppUser user)   
        {
            // Türkçe format.

            var keyString = _configuration["Jwt:Key"]
                ?? throw new Exception("JWT Key missing in configuration");

            var issuer = _configuration["Jwt:Issuer"]
                ?? throw new Exception("JWT Issuer missing");

            var audience = _configuration["Jwt:Audience"]
                ?? throw new Exception("JWT Audience missing");

            var expireTime = _configuration["Jwt:ExpireTime"]
                ?? throw new Exception("JWT ExpireTime missing");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? ""),
                new Claim("fullname", user.FullName ?? "")
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(expireTime)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}
