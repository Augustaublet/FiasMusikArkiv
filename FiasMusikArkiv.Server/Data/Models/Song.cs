using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiasMusikArkiv.Server.Data.Models
{
    public class Song : BaseModel
    {
        [MaxLength(255)]
        public String Name { get; set; }
        public String? Description { get; set; }
        [ForeignKey("Genres")]
        public virtual GenreCode Genre { get; set; }
    }
}
