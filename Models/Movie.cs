using System.ComponentModel.DataAnnotations;

namespace Movies.Api.Models
{
    public class Movie
    {
        public  int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string Title { get; set; } = string.Empty;
        [StringLength(250)]
        public string? Description { get; set; } = string.Empty;
        [Range(1888, 2023)]
        public int ReleaseYear { get; set; }

        [Range(1, 10)]
        public decimal Rating { get; set; }

        [Required]
        [StringLength(100)]
        public string Genre { get; set; } = string.Empty;
        public string? PosterUrl { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
