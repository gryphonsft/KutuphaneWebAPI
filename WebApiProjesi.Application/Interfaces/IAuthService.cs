using WebApiProjesi.Application.DTOs.Requests;
using WebApiProjesi.Application.DTOs.Respones;

namespace WebApiProjesi.Application.Interfaces
{
    public interface IAuthService
    {
        #region User authentication servisi
        Task<(bool Success, string Message, Guid? UserId)> LoginAsync(LoginUserDto dto);
        Task<(bool Success, string Message)> RegisterAsync(RegisterUserDto dto);
        #endregion

        #region Read servisi
        Task<List<UserDto>> GetAllUserAsync();
        #endregion
    }
}
