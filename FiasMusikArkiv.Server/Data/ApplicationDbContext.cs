using Microsoft.EntityFrameworkCore;

namespace FiasMusikArkiv.Server.Data
{
    public class FiasMusikArkivDbContext : DbContext
    {
        public FiasMusikArkivDbContext(DbContextOptions<FiasMusikArkivDbContext> options) : base(options) { }
    }
}
