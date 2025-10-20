using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProjesi.Application.DTOs.Requests;
using WebApiProjesi.Application.DTOs.Respones;

namespace WebApiProjesi.Application.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string Message, Guid? UserId)> LoginAsync(LoginUserDto dto);
        Task<(bool Success, string Message)> RegisterAsync(RegisterUserDto dto);
        Task<List<UserDto>> GetAllUserAsync();
    }
}
