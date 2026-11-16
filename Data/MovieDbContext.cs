using Microsoft.EntityFrameworkCore;

namespace Movies.Api.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base (options)
        {
            // Initialize the database context
        }
        public DbSet<Models.Movie> Movies
        {
            get; set;
        }
    }
}
