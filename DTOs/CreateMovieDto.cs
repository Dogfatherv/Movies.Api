using System.ComponentModel.DataAnnotations;

namespace Movies.Api.DTOs
{
    public class CreateMovieDto
    {
        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? Description { get; set; } = string.Empty;
        [Range(1, 1000)]
        public int ReleaseYear { get; set; }
        [Range(1888, 2026)]  
        public decimal Rating { get; set; }
        [Required]
        [StringLength(100)]
        public string Genre { get; set; } = string.Empty;
        [StringLength(200)] 
        public string? PosterUrl { get; set; }

        [Range(1, 1000)]
        public int DurationMinutes { get; set; }
    }
}
