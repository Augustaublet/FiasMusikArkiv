namespace FiasMusikArkiv.Server.Data.DTOs
{
    public abstract class _baseDto
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
