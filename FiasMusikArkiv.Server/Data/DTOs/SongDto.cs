using System.ComponentModel.DataAnnotations;

namespace FiasMusikArkiv.Server.Data.DTOs
{
    public class SongDto
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Genre { get; set; } // Assuming GenreCode has an integer Id property
    }
}
