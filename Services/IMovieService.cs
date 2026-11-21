using Movies.Api.DTOs;

namespace Movies.Api.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieResponseDto>> GetAllAsync( string? search = null, string? genre = null, string? sortby = null, int pageNumber = 1, int pageSize = 10);
        Task<MovieResponseDto?> GetByIdAsync(int id);
        Task<MovieResponseDto> CreateAsync(CreateMovieDto dto);
        Task<bool> UpdateAsync(int id, MovieUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
