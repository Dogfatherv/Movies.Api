
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Services;
using Movies.Api.DTOs;

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpGet]
        public async Task<ActionResult<PagedResultDto<MovieResponseDto>>> GetMovies(
            [FromQuery] string? search,
            [FromQuery] string? genre,
            [FromQuery] string? sortBy,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
            )
        {
            var movies = await _movieService.GetAllAsync(
                search,
                genre,
                sortBy,
                pageNumber,
                pageSize
                );
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieResponseDto?>> GetMovie(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound("Movie not found.");
            }
            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<MovieResponseDto>> CreateMovie(CreateMovieDto dto)
        {
            var movie = await _movieService.CreateAsync(dto);
            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MovieResponseDto>> UpdateMovie(int id, MovieUpdateDto dto)
        {
            var updated = await _movieService.UpdateAsync(id, dto);
            if (!updated)
            {
                return NotFound("Movie not found.");
            }
            return NoContent();

            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.Movie>> DeleteMovie(int id)
        {
            var deleted = await _movieService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound("Movie not found.");
            }
            return NoContent();
        }

    }
}

              
            
        

   

