using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiasMusikArkiv.Server.Data.Models
{
    public abstract class CodeBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(32)]
        public virtual string Code { get; set; }

        [MaxLength(128)]
        public string Description { get; set; }

        public int SortOrder { get; set; }
    }
}
