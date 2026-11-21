
using Movies.Api.DTOs.Auth;

namespace Movies.Api.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto); 
    }
}
