using System;
using System.ComponentModel.DataAnnotations;

namespace MusicApi.Models
{
    public class Song
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Range(0, double.MaxValue)]
        public float Duration { get; set; }

        [DataType(DataType.Date)]
        public DateTime UploadedDate { get; set; }

        public bool IsFeatured { get; set; }

        // Navigation properties
        public int ArtistId { get; set; }
        public Artist? Artist { get; set; }

        public int AlbumId { get; set; }
        public Album? Album { get; set; }
    }
}
