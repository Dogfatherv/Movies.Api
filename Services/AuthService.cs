using Movies.Api.DTOs.Auth;
using Movies.Api.Models;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;


namespace Movies.Api.Services
{
    public class AuthService : IAuthService
    {
        public Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
