using FiasMusikArkiv.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FiasMusikArkiv.Server.Data
{
    public class FiasMusikArkivDbContext : DbContext
    {
        public FiasMusikArkivDbContext(DbContextOptions<FiasMusikArkivDbContext> options) : base(options) { }
        private const string DefaultCreatedBy = "(System)";
        private const string DefaultModifiedBy = "(System)";
        private const string DefaultDeletedBy = "(System)";

        public DbSet<Song> Songs { get; set;}    
        
    }
}
