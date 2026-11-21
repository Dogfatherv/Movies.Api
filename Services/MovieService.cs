using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;
using Movies.Api.DTOs;
using Movies.Api.Models;


namespace Movies.Api.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _context;

        public MovieService(MovieDbContext context)
        {
            _context = context;
        }

        // Note: Update IMovieService to return Task<PagedResultDto<MovieResponseDto>>
        public async Task<PagedResultDto<MovieResponseDto>> GetAllAsync(
            string? search = null,
            string? genre = null,
            string? sortby = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var query = _context.Movies.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(m => m.Title.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                query = query.Where(m => m.Genre == genre);
            }

            query = sortby?.ToLower() switch
            {
                "rating" => query.OrderByDescending(m => m.Rating),
                "year" => query.OrderByDescending(m => m.ReleaseYear),
                "title" => query.OrderBy(m => m.Title), // Fixed lowercase "title"
                _ => query.OrderBy(m => m.Id)
            };

            var totalRecords = await query.CountAsync();

            var movies = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new MovieResponseDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    Genre = m.Genre,
                    ReleaseYear = m.ReleaseYear,
                    Rating = m.Rating,
                    DurationMinutes = (int)m.DurationMinutes,
                    CreatedAt = m.CreatedAt
                })
                .ToListAsync();

            return new PagedResultDto<MovieResponseDto>
            {
                Items = movies,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };
        }

        public async Task<MovieResponseDto?> GetByIdAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return null;

            return MapToDto(movie);
        }

        public async Task<MovieResponseDto> CreateAsync(CreateMovieDto dto)
        {
            var movie = new Movie
            {
                Title = dto.Title,
                Description = dto.Description,
                Genre = dto.Genre,
                ReleaseYear = dto.ReleaseYear,
                Rating = dto.Rating,
                DurationMinutes = dto.DurationMinutes,
                CreatedAt = DateTime.UtcNow
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return MapToDto(movie);
        }

        public async Task<bool> UpdateAsync(int id, MovieUpdateDto dto)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return false;

            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.Genre = dto.Genre;
            movie.ReleaseYear = (int)dto.ReleaseYear;
            movie.Rating = (int)dto.Rating;
            movie.DurationMinutes = dto.DurationMinutes;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return false;

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        private static MovieResponseDto MapToDto(Movie movie)
        {
            return new MovieResponseDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre,
                ReleaseYear = movie.ReleaseYear,
                Rating = movie.Rating,
                DurationMinutes = (int)movie.DurationMinutes,
                CreatedAt = movie.CreatedAt
            };
        }

        Task<IEnumerable<MovieResponseDto>> IMovieService.GetAllAsync(string? search, string? genre, string? sortby, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}













