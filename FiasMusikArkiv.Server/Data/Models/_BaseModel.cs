using System.ComponentModel.DataAnnotations;

namespace FiasMusikArkiv.Server.Data.Models
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        [MaxLength(64)]
        public string CreatedBy { get; set; }

        /*public DateTime? ModifiedAt { get; set; }
        [MaxLength(64)]
        public string ModifiedBy { get; set; }*/
    }
}
