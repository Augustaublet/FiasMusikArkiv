using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiasMusikArkiv.Server.Data.Models
{
    public class Song : BaseModel
    {
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(512)]
        public string? Description { get; set; }
        [ForeignKey("Genres")]
        public virtual GenreCode Genre { get; set; }
    }
}
