
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;
using Movies.Api.Models;
using Movies.Api.DTOs;

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly Data.MovieDbContext _context;
        public MoviesController(Data.MovieDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Movie>>> GetMovies()
        {
            var movies = await _context.Movies.ToListAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound("Movie not found.");
            }
            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<Models.Movie>> CreateMovie(Models.Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Models.Movie>> UpdateMovie(int id, Models.Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest("ID mismatch.");
            }

            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.Movie>> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return Ok(NoContent());
        }

    }
}
