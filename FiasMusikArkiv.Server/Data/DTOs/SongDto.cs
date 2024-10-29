using System.ComponentModel.DataAnnotations;

namespace FiasMusikArkiv.Server.Data.DTOs
{
    public class SongDto : _baseDto
    {
        [MaxLength(255)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Genre { get; set; } 
    }
}
